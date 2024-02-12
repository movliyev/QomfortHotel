﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using QomfortHotelFinal.Areas.Admin.ViewModels;
using QomfortHotelFinal.DAL;
using QomfortHotelFinal.Models;
using QomfortHotelFinal.Utilities.Extensions;

namespace QomfortHotelFinal.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class RoomController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public RoomController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //[Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) return BadRequest();

            int count = await _context.Rooms.CountAsync();

            List<Room> Rooms = await _context.Rooms.Skip((page - 1) * 3).Take(3)
                .Include(p => p.Category)
                .Include(p => p.RoomImages.Where(pi => pi.IsPrimary == true))
                .Include(p => p.RoomServicees)
                .ThenInclude(pt => pt.Servicee)
                 .Include(p => p.RoomFacilities)
                .ThenInclude(pt => pt.Facility)
                .ToListAsync();

            PaginateVM<Room> pagvm = new PaginateVM<Room>
            {
                Items = Rooms,
                TotalPage = Math.Ceiling((double)count / 3),
                CurrentPage = page,
            };

            return View(pagvm);
        }
        //[Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Create()
        {
            CreateRoomVM vm = new CreateRoomVM();

            vm.Categorys = await _context.Categories.ToListAsync();
            vm.Facilitiys = await _context.Facilities.ToListAsync();
            vm.Servicees = await _context.Servisees.ToListAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categorys = await _context.Categories.ToListAsync();
                vm.Facilitiys = await _context.Facilities.ToListAsync();
                vm.Servicees = await _context.Servisees.ToListAsync();
                return View(vm);
            }
            bool result = await _context.Categories.AnyAsync(c => c.Id == vm.CategoryId);

            if (!result)
            {
                vm.Categorys = await _context.Categories.ToListAsync();
                vm.Facilitiys = await _context.Facilities.ToListAsync();
                vm.Servicees = await _context.Servisees.ToListAsync();
                ModelState.AddModelError("CategoryId", "Bu id li category movcud deyil");
                return View(vm);
            }
            foreach (int item in vm.Serviceeids)
            {
                bool Serviceeresult = await _context.Servisees.AnyAsync(t => t.Id == item);
                if (!Serviceeresult)
                {
                    vm.Categorys = await _context.Categories.ToListAsync();
                    vm.Facilitiys = await _context.Facilities.ToListAsync();
                    vm.Servicees = await _context.Servisees.ToListAsync();
                    ModelState.AddModelError("ServiceeIds", "Bu id li Servicee movcud deyil");
                    return View(vm);
                }
            }
            foreach (int item in vm.Facilityids)
            {
                bool cresult = await _context.Facilities.AnyAsync(t => t.Id == item);
                if (!cresult)
                {
                    vm.Categorys = await _context.Categories.ToListAsync();
                    vm.Facilitiys = await _context.Facilities.ToListAsync();
                    vm.Servicees = await _context.Servisees.ToListAsync();
                    ModelState.AddModelError("Facilityids", "Bu id li Facility movcud deyil");
                    return View(vm);
                }
            }
           

            if (!vm.MainPhoto.ValidateType("image/"))
            {
                vm.Categorys = await _context.Categories.ToListAsync();
                vm.Facilitiys = await _context.Facilities.ToListAsync();
                vm.Servicees = await _context.Servisees.ToListAsync();
                ModelState.AddModelError("MainPhoto", "Fayl tipi uyqun deyil");
                return View(vm);
            }

            if (!vm.MainPhoto.ValidateSize(600))
            {
                vm.Categorys = await _context.Categories.ToListAsync();
                vm.Facilitiys = await _context.Facilities.ToListAsync();
                vm.Servicees = await _context.Servisees.ToListAsync();
                ModelState.AddModelError("MainPhoto", "Fayl olcusu uyqun deyil");
                return View(vm);

            }
           

            RoomImage main = new RoomImage
            {
                IsPrimary = true,
                Url = await vm.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "rooms")

            };
            //if (vm.Price <= 0&&vm.Bed<=0&&vm.Size<=0&&vm.Capacity<=0)
            //{
            //    ModelState.AddModelError("Price,Bed,Size,Capacity", "there is no negative or zero quantity");
            //}
            Room Room = new Room
            {
                
                Name=vm.Name,
                Description=vm.Description, 
                DetailDescription=vm.DetailDescription,
                Price=vm.Price,
                Size=vm.Size,
                Bed=vm.Bed,
                Capacity=vm.Capacity,
                BathRoom=vm.BathRoom,   
                CategoryId = (int)vm.CategoryId,
                RoomServicees = new List<RoomService>(),
                RoomFacilities = new List<RoomFacility>(),
                RoomImages = new List<RoomImage> { main}



            };

            TempData["Message"] = "";

            foreach (IFormFile photo in vm.Photos ?? new List<IFormFile>())
            {
                if (!photo.ValidateType("image/"))
                {
                    TempData["Message"] += $" <p class=\"text-danger\">{photo.FileName}  file tipi uyqun deyil</p>";
                    continue;
                }
                if (!photo.ValidateSize(600))
                {
                    TempData["Message"] += $"<p class=\"text-danger\">{photo.FileName}  file olcusu uyqun deyil</p>";

                    continue;
                }

                Room.RoomImages.Add(new RoomImage
                {
                    IsPrimary = null,
                    Url = await photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "rooms")

                });

            }

            foreach (var item in vm.Serviceeids)
            {
                RoomService pServicee = new RoomService
                {
                    ServiceId = item,
                };
                Room.RoomServicees.Add(pServicee);
            }
            foreach (var item in vm.Facilityids)
            {
                RoomFacility cServicee = new RoomFacility
                {
                    FacilityId = item,
                };
                Room.RoomFacilities.Add(cServicee);
            }
           
            await _context.Rooms.AddAsync(Room);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //[Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Room Room = await _context.Rooms
                .Include(p => p.RoomServicees)
                .Include(p => p.RoomImages)
                .Include(p => p.RoomFacilities)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (Room == null) return NotFound();

            UpdateRoomVM vm = new UpdateRoomVM
            {
                Name = Room.Name,
                Price = Room.Price,
                Bed=Room.Bed,   
                Size = Room.Size,
                Capacity = Room.Capacity,
                BathRoom=Room.BathRoom,  
                Description = Room.Description,
               DetailDescription = Room.DetailDescription,  
                CategoryId = Room.CategoryId,
                RoomImages = Room.RoomImages,
                Serviceeids = Room.RoomServicees.Select(p => p.ServiceId).ToList(),
                Facilityids = Room.RoomFacilities.Select(p => p.FacilityId).ToList(),
                Categorys = await _context.Categories.ToListAsync(),
                Servicees = await _context.Servisees.ToListAsync(),
                Facilitiys = await _context.Facilities.ToListAsync(),

            };


            await _context.SaveChangesAsync();


            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateRoomVM pvm)
        {
            if (id <= 0) return BadRequest();
            Room exsist = await _context.Rooms
              .Include(p => p.RoomServicees)
              .Include(p => p.RoomFacilities)
              .Include(p => p.RoomImages)
              .FirstOrDefaultAsync(p => p.Id == id);
            if (exsist == null)return NotFound();   

            if (!ModelState.IsValid)
            {
                pvm.Categorys = await _context.Categories.ToListAsync();
                pvm.Servicees = await _context.Servisees.ToListAsync();
                pvm.Facilitiys = await _context.Facilities.ToListAsync();
                pvm.RoomImages = exsist.RoomImages;
                return View(pvm);
            }

            if (exsist == null) return NotFound();
            if (pvm.MainPhoto is not null)
            {
                if (!pvm.MainPhoto.ValidateType("image/"))
                {
                    pvm.Categorys = await _context.Categories.ToListAsync();
                    pvm.Servicees = await _context.Servisees.ToListAsync();
                    pvm.Facilitiys = await _context.Facilities.ToListAsync();
                    pvm.RoomImages = exsist.RoomImages;
                    ModelState.AddModelError("MainPhoto", "Fayl tipi uyqun deyil");
                    return View(pvm);
                }

                if (!pvm.MainPhoto.ValidateSize(600))
                {
                    pvm.Categorys = await _context.Categories.ToListAsync();
                    pvm.Servicees = await _context.Servisees.ToListAsync();
                    pvm.Facilitiys = await _context.Facilities.ToListAsync();
                    pvm.RoomImages = exsist.RoomImages;

                    ModelState.AddModelError("MainPhoto", "Fayl olcusu uyqun deyil");
                    return View(pvm);

                }

            }




            bool result = await _context.Categories.AnyAsync(c => c.Id == pvm.CategoryId);
            if (!result)
            {
                pvm.Categorys = await _context.Categories.ToListAsync();
                pvm.Servicees = await _context.Servisees.ToListAsync();
                pvm.Facilitiys = await _context.Facilities.ToListAsync();
                pvm.RoomImages = exsist.RoomImages;

                ModelState.AddModelError("CategoryId", "Bele bir category movcud deyil");
                return View(pvm);
            }

            exsist.RoomServicees.RemoveAll(pt => !pvm.Serviceeids.Exists(tid => tid == pt.ServiceId));


            List<int> create = pvm.Serviceeids.Where(tid => !exsist.RoomServicees.Exists(pt => pt.ServiceId == tid)).ToList();
            foreach (var tid in create)
            {
                bool tresult = await _context.Servisees.AnyAsync(t => t.Id == tid);
                if (!tresult)
                {
                    pvm.Categorys = await _context.Categories.ToListAsync();
                    pvm.Servicees = await _context.Servisees.ToListAsync();
                    pvm.Facilitiys = await _context.Facilities.ToListAsync();
                    pvm.RoomImages = exsist.RoomImages;

                    ModelState.AddModelError("CategoryId", "Bele bir Servicee movcud deyil");
                    return View(pvm);
                }
                exsist.RoomServicees.Add(new RoomService { ServiceId = tid });
            }


            foreach (var item in exsist.RoomFacilities)
            {
                if (!pvm.Facilityids.Exists(t => t == item.FacilityId))
                {
                    _context.RoomFacilities.Remove(item);
                }
            }

            foreach (int item in pvm.Facilityids)
            {
                if (!exsist.RoomFacilities.Any(p => p.FacilityId == item))
                {
                    exsist.RoomFacilities.Add(new RoomFacility
                    { FacilityId = item });

                }
            }
            foreach (var item in exsist.RoomServicees)
            {
                if (!pvm.Serviceeids.Exists(t => t == item.ServiceId))
                {
                    _context.RoomServices.Remove(item);
                }
            }

            foreach (int item in pvm.Serviceeids)
            {
                if (!exsist.RoomServicees.Any(p => p.ServiceId == item))
                {
                    exsist.RoomServicees.Add(new RoomService
                    { ServiceId = item });

                }
            }

            if (pvm.MainPhoto != null)
            {
                string fileNAme = await pvm.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "rooms");

                RoomImage eximg = exsist.RoomImages.FirstOrDefault(p => p.IsPrimary == true);
                eximg.Url.DeleteFile(_env.WebRootPath, "assets", "images", "rooms");
                exsist.RoomImages.Remove(eximg);

                exsist.RoomImages.Add(new RoomImage
                {
                    IsPrimary = true,
                    Url = pvm.Name
                });
            }
           

            if (pvm.Imageids is null)
            {
                pvm.Imageids = new List<int>();
            }
            List<RoomImage> remov = exsist.RoomImages.Where(p => !pvm.Imageids.Exists(imgId => imgId == p.Id) && p.IsPrimary == null).ToList();
            foreach (RoomImage item in remov)
            {
                item.Url.DeleteFile(_env.WebRootPath, "assets", "images", "rooms");
                exsist.RoomImages.Remove(item);
            }

            TempData["Message"] = "";

            foreach (IFormFile photo in pvm.Photos ?? new List<IFormFile>())
            {
                if (!photo.ValidateType("image/"))
                {
                    TempData["Message"] += $" <p class=\"text-danger\">{photo.FileName}  file tipi uyqun deyil</p>";
                    continue;
                }
                if (!photo.ValidateSize(600))
                {
                    TempData["Message"] += $"<p class=\"text-danger\">{photo.FileName}  file olcusu uyqun deyil</p>";

                    continue;
                }

                exsist.RoomImages.Add(new RoomImage
                {
                    IsPrimary = null,
                    Url = await photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "rooms")

                });

            }

            exsist.Name = pvm.Name;
            exsist.Description = pvm.Description;
            exsist.Price = pvm.Price;
            exsist.CategoryId = pvm.CategoryId;
            exsist.Bed= pvm.Bed;
            exsist.BathRoom = pvm.BathRoom; 
            exsist.DetailDescription = pvm.DetailDescription;   
            exsist.Capacity = pvm.Capacity;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Room Room = await _context.Rooms.Include(p => p.RoomImages).FirstOrDefaultAsync(p => p.Id == id);
            if (Room == null) return NotFound();
            foreach (var item in Room.RoomImages ?? new List<RoomImage>())
            {
                item.Url.DeleteFile(_env.WebRootPath, "assets", "images", "rooms");
            }
            _context.Rooms.Remove(Room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }






        //public async Task<IActionResult> Detail(int id)
        //{
        //    if (id == 0) return BadRequest();

        //    Room Room = await _context.Rooms
        //        .Include(p => p.Category)
        //        .Include(p => p.RoomImages)
        //        .Include(p => p.RoomServicees).ThenInclude(x => x.Servicee)
        //        .Include(p => p.RoomFacilitys).ThenInclude(x => x.Facility)
        //        .Include(p => p.RoomSizes).ThenInclude(x => x.Size)
        //        .FirstOrDefaultAsync(x => x.Id == id);



        //    if (Room == null) return NotFound();
        //    ViewBag.RoomServicees = await _context.RoomServicees.ToListAsync();
        //    ViewBag.RoomFacilitys = await _context.RoomServicees.ToListAsync();
        //    ViewBag.RoomSizes = await _context.RoomServicees.ToListAsync();



        //    return View(Room);
        //}


    }
}