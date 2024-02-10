var animation = null;

var animations = ['email', 'password'];

function showContainer(name) {
    animations
        .filter(a => a != name)
        .forEach(a => {
            document
                .querySelector('.fc-avatar-' + a)
                .classList.remove('d-block');
            document.querySelector('.fc-avatar-' + a).classList.add('d-none');
        });

    document.querySelector('.fc-avatar-' + name).classList.remove('d-none');
    document.querySelector('.fc-avatar-' + name).classList.add('d-block');
}

function bindEmailAnimation() {
    requestAnimationFrame(() => {
        unbindAnimation();
        showContainer('email');
        animation = lottie.loadAnimation({
            container: document.querySelector('.fc-avatar-email'),
            renderer: 'svg',
            loop: false,
            autoplay: false,
            animationData: emailAnimationData,
        });
        animation.setSubframe(true);
    });
}

function bindPasswordAnimation() {
    requestAnimationFrame(() => {
        unbindAnimation();
        showContainer('password');
        animation = lottie.loadAnimation({
            container: document.querySelector('.fc-avatar-password'),
            renderer: 'svg',
            loop: false,
            autoplay: true,
            animationData: passwordAnimationData,
        });
    });
}

function unbindAnimation() {
    if (animation) {
        animation.setDirection(-1);
        animation.goToAndStop(0, true);
        animation.destroy();
        animation = null;
    }
}

function handleEmailAnimation(e) {
    var input = e.target;
    var span = document.createElement('span');
    var box = input.getBoundingClientRect();
    span.style = input.style;
    span.style.position = 'absolute';
    span.textContent = input.value;
    document.body.appendChild(span);
    spanWidth = span.getBoundingClientRect().width;
    span.remove();
    var percent = (spanWidth * 100) / box.width;

    var total = animation.getDuration(true);
    var next = Math.ceil((total * percent) / 100);
    if (next > total) {
        next = total;
    }
    if (next < 0) {
        next = 0;
    }
    animation.goToAndStop(next, true);
    console.log(next, total);
}

$(function() {
    $('input[type="email"]')
        .on('focus', bindEmailAnimation)
        .on('keydown', handleEmailAnimation);

    $('input[type="password"]')
        .on('focus', bindPasswordAnimation)
        .on('blur', bindEmailAnimation);
});
