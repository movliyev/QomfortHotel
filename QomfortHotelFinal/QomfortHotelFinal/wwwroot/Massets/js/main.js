const deletebtns = document.querySelectorAll(".customDeletebtn");

deletebtns.forEach(btn => {
    btn.addEventListener("click", function (e) {
        e.preventDefault();
        var endpoint = btn.getAttribute("href");
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(endpoint)
                    .then(response => response.json())
                    .then(data => {
                        if (data.status == 200) {
                            window.location.reload(true);
                        }
                        else {
                            Swal.fire({
                                title: "Error!",
                                text: "Your file has not been deleted.",
                                icon: "error"
                            });
                        }
                    })

            }
            
        });
    })
})




