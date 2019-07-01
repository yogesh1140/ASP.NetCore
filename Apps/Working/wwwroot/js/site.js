

(function (d) {
    $('#username').text('Stella Cox');
    ////var ele = document.getElementById('username')
    ////ele.innerHTML = "Stella Cox"

    //$('#main').on('mouseenter',function () {
    //    main.style.backgroundColor = '#888'
    //})
    ////var main = document.getElementById('main')
    ////main.onmouseenter = function () {
    ////    main.style.backgroundColor = '#888'
    ////}

    //main.onmouseleave = function () {
    //    main.style.backgroundColor = ''
    //}
    //$('ul.menu li a').on('click', function () {
    //    alert($(this).text())
    //})
    
    $('#sidebartoggle').on('click', function(){
        $('#sidebar,#wrapper').toggleClass('hide-sidebar')
        if ($('#sidebar,#wrapper').hasClass('hide-sidebar')) {
            $('#sidebartoggle i.fa').removeClass('fa-angle-left')
            $('#sidebartoggle i.fa').addClass('fa-angle-right')


        } else {
            $('#sidebartoggle i.fa').addClass('fa-angle-left')
            $('#sidebartoggle i.fa').removeClass('fa-angle-right')
        }
    })

})('param')
