
/*跳转页面后，突显对应的左侧菜单*/

function Feature(index) {
    $("#nav-accordion a").removeClass("current"); //删除左侧菜单的Class
    $("#nav-accordion a").eq(index).addClass("current"); //将对应菜单的Class设置为active
}