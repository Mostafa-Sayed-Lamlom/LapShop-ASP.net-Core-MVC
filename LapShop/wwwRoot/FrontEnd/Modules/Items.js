
var ClsItems = {
    GetAll: function () {
        Helper.AjaxCallGet("https://localhost:44397/api/Items/GetAll", {}, "json",
            function (data) {


                $('#ItemPagination').pagination({
                    dataSource: data.data,
                    pageSize: 20,
                    showGoInput: true,
                    showGoButton: true,
                    callback: function (data, pagination) {
                        // template method of yourself
                        var htmlData = "";
                        for (var i = 0; i < data.length; i++) {
                            htmlData += ClsItems.DrawItem(data[i]);
                        }

                        var d1 = document.getElementById('ItemArea');
                        d1.innerHTML = htmlData;
                    }
                });
            }, function () { });
    },
    DrawItem: function (item) {
        var data = "<div class='col-xl-3 col-6 col-grid-box'>";
        data += "<div class='product-box'><div class='img-wrapper'>";
        data += "<div class='front'> <img src='/Admin/upload/images/items/" + item.imageName + "' class='img-fluid blur-up lazyload bg-img' alt=''></div>";
        data += "<div class='back'> <img src='/Admin/upload/images/items/" + item.imageName + "' class='img-fluid blur-up lazyload bg-img' alt=''></div>";
        data += "</div>";
        data += "<div class='product-detail'><div class='rating'> <i class='fa fa-star'></i> <i class='fa fa-star'></i> <i class='fa fa-star'></i>";
        data += "<i class='fa fa-star'></i> <i class='fa fa-star'></i></div>";
        data += "<h6>" + item.itemName + "</h6> <p> </p>";
        data += "<h4>$" + item.salesPrice + "</h4>";
        data += "<ul class='color-variant'><li class='bg-light0'></li><li class='bg-light1'></li><li class='bg-light2'></li></ul> </div> </div> </div>";
        return data;
    }
}

