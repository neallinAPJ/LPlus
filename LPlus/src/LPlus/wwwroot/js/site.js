var ajaxMethod = {};
ajaxMethod.post = function (url, data, handler, dataType, hasChildAjaxRequest) {
    var paramObj = {
        url: url,
        data: data,
        type: "POST",
        beforeSend: function () {
        },
        success: function (result) {
            handler(result);
        },
        error: function () {
        },
        complete: function () {
        }
    };

    if (dataType != undefined && dataType != null) {
        paramObj.dataType = dataType;
    }
    $.ajax(paramObj);
}
ajaxMethod.get = function (url, data, handler, dataType, hasChildAjaxRequest) {
    var paramObj = {
        url: url,
        data: data,
        type: "GET",
        beforeSend: function () {

        },
        success: function (result) {
            handler(result);
        },
        error: function () {
        },
        complete: function () {
        }
    };

    if (dataType != undefined && dataType != null) {
        paramObj.dataType = dataType;
    }

    $.ajax(paramObj);
}

var getLocation=function()
{
    setLocation();
    var loction = JSON.parse(Cookies.getCookie("VPIAO_MOBILE_CURRENTCITY")).name;
    if (loction == null || loction == "" || loction == undefined) {
        loction = JSON.parse(Cookies.getCookie("VPIAO_MOBILE_DEFAULTCITY")).name;
    }
    return loction;
}
var setLocation = function (successFunc, errorFunc) { //successFunc获取定位成功回调函数，errorFunc获取定位失败回调
    //首先设置默认城市
    var defCity = {
        id: '000001',
        name: '北京市',
        date: curDateTime()//获取当前时间方法
    };
    //默认城市
    Cookies.setCookie('VPIAO_MOBILE_DEFAULTCITY', JSON.stringify(defCity));

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var lat = position.coords.latitude;
            var lon = position.coords.longitude;
            //var map = new BMap.Map("container");   // 创建Map实例
            var point = new BMap.Point(lon, lat); // 创建点坐标
            var gc = new BMap.Geocoder();
            gc.getLocation(point, function (rs) {
                var addComp = rs.addressComponents;
                var curCity = {
                    id: '',
                    name: addComp.city,
                    date: curDateTime()
                };
                //当前定位城市
                Cookies.setCookie('VPIAO_MOBILE_CURRENTCITY', JSON.stringify(curCity));
                if (successFunc != undefined)
                    successFunc(addComp);
            });
        },
        function (error) {
            switch (error.code) {
                case 1:
                    alert("位置服务被拒绝。");
                    break;
                case 2:
                    alert("暂时获取不到位置信息。");
                    break;
                case 3:
                    alert("获取位置信息超时。");
                    break;
                default:
                    alert("未知错误。");
                    break;
            }
            var curCity = {
                id: '000001',
                name: '北京市',
                date: curDateTime()
            };
            //默认城市
            Cookies.setCookie('VPIAO_MOBILE_DEFAULTCITY', JSON.stringify(curCity));
            if (errorFunc != undefined)
                errorFunc(error);
        }, { timeout: 5000, enableHighAccuracy: true });
    } else {
        alert("你的浏览器不支持获取地理位置信息。");
        if (errorFunc != undefined)
            errorFunc("你的浏览器不支持获取地理位置信息。");
    }
};
var showPosition = function (position) {
    var lat = position.coords.latitude;
    var lon = position.coords.longitude;
    //var map = new BMap.Map("container");   // 创建Map实例
    var point = new BMap.Point(lon, lat); // 创建点坐标
    var gc = new BMap.Geocoder();
    gc.getLocation(point, function (rs) {
        var addComp = rs.addressComponents;
        var curCity = {
            id: '',
            name: addComp.city,
            date: curDateTime()
        };
        //当前定位城市
        Cookies.setCookie('VPIAO_MOBILE_CURRENTCITY', JSON.stringify(curCity));
        //alert(addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street);
    });
};
var showPositionError = function (error) {
    switch (error.code) {
        case 1:
            alert("位置服务被拒绝。");
            break;
        case 2:
            alert("暂时获取不到位置信息。");
            break;
        case 3:
            alert("获取位置信息超时。");
            break;
        default:
            alert("未知错误。");
            break;
    }
    var curCity = {
        id: '000001',
        name: '北京市',
        date: curDateTime()
    };
    //默认城市
    Cookies.setCookie('VPIAO_MOBILE_DEFAULTCITY', JSON.stringify(curCity));
};
var curDateTime=function()
{
    var myDate = new Date();
    return myDate.toLocaleString();
}
var Cookies = {
}
Cookies.setCookie=function(name, value) {
    var Days = 30;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}
Cookies.getCookie = function (name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}
Cookies.delCookie=function (name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null)
        document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}
