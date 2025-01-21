



//new Chart(ctx, {
//    type: 'doughnut',
//    data: {
//        labels: [
//            'Red',
//            'Blue',
//            'Yellow'
//        ],
//        datasets: [{
//            label: 'My First Dataset',
//            data: [300, 50, 100],
//            backgroundColor: [
//                'rgb(255, 99, 132)',
//                'rgb(54, 162, 235)',
//                'rgb(255, 205, 86)'
//            ],
//            hoverOffset: 4
//        }]
//    },

//});


//const labels = Utils.months({ count: 7 });
//const data = {
//    labels: labels,
//    datasets: [{
//        label: 'My First Dataset',
//        data: [65, 59, 80, 81, 56, 55, 40],
//        backgroundColor: [
//            'rgba(255, 99, 132, 0.2)',
//            'rgba(255, 159, 64, 0.2)',
//            'rgba(255, 205, 86, 0.2)',
//            'rgba(75, 192, 192, 0.2)',
//            'rgba(54, 162, 235, 0.2)',
//            'rgba(153, 102, 255, 0.2)',
//            'rgba(201, 203, 207, 0.2)'
//        ],
//        borderColor: [
//            'rgb(255, 99, 132)',
//            'rgb(255, 159, 64)',
//            'rgb(255, 205, 86)',
//            'rgb(75, 192, 192)',
//            'rgb(54, 162, 235)',
//            'rgb(153, 102, 255)',
//            'rgb(201, 203, 207)'
//        ],
//        borderWidth: 1
//    }]
//};
let count = 0;
function countPro() {
    let brandArray = [];
    let countBrand=[]
    $.ajax({
        url: "/CountProducts/Count",
        type: "GET",
        dataType: "JSON",
        success: function (data) {

            console.log(data.brand)
            countBrand = data.coutBrand
            $.each(data.brand, function (ind, val)
            {

                brandArray.push(val['brand_name']);
            })
            console.log(brandArray)

            count = data.count
            const ctx = document.getElementById('myChart');

            new Chart(ctx, {
                type: 'bar',
                data: {

                    labels: ['PRODUCTS', ...brandArray],
                    datasets: [
                        {
                            label: count,
                            data: [count, ...countBrand],
                            //label: '# of Votes',
                            backgroundColor: [
                                '#000dd0',
                               	'#0075d0 ',
                                '#0DD000',
                                'rgba(255, 205, 86, 0.2)',
                                'rgba(75, 192, 192, 0.2)',
                            ],
                        },

                    ]
                },

                options: {
                    plugins: {
                        title: {
                            display: true,
                            text: 'Products Chart '

                        }
                    }  , animations: {
                        x: { // Animates the x-axis
                            duration: 2000,
                            easing: 'easeInOutBounce', // Customize the easing function
                            loop: false
                        },
                        y: { // Animates the y-axis
                            duration: 2000,
                            easing: 'easeOutElastic', // Customize the easing function
                            loop: false
                        },
                        tension: { // Smooth transition of line tension
                            duration: 1000,
                            easing: 'linear',
                            from: 1,
                            to: 0,
                            loop: true
                        }
                    },
                    //x: {
                    //    ticks: {
                    //        display: false // Disable x-axis labels
                    //    }
                    //},
                    //y: {
                    //    beginAtZero: true,
                    //    ticks: {
                    //        display: false // Disable y-axis labels
                    //    }
                    //}
                }

            });
        }
    })
}

console.log(count)
 const brands = ['a', 'b', 'c'];

countPro()






/*const data = {
    labels: [
        'Red',
        'Blue',
        'Yellow'
    ],
    datasets: [{
        label: 'My First Dataset',
        data: [300, 50, 100],
        backgroundColor: [
            'rgb(255, 99, 132)',
            'rgb(54, 162, 235)',
            'rgb(255, 205, 86)'
        ],
        hoverOffset: 4
    }]
};*/





$(document).ready(function () {

    $("#export").click(function () {

        var table2excel = new Table2Excel();
        table2excel.export(document.querySelectorAll("#tblData"));


    })

    //action export , print ...
    new DataTable('#tblData', {

        //layout: {
        //    //dom: '<"top"B>rt<"bottom"ip>',

        //    topStart: {
        //        buttons: ['copy', 'csv', 'excel', 'pdf', 'print']
        //    }
        //}


        dom: '<"top"B>rt<"bottom"ip>',
    
        buttons: ['copy', 'csv', 'excel', 'pdf', 'print'],
        paging: false,
        info: true,

  
       
    });


 

         


    $("#searchBrand").change(function () {
        let frm = $("#search").serialize()
        $.ajax({
            url: "/PhoneDetail/SearchBrand",
            data: frm,
            type: "POST",
            dataType: "JSON",
            success: function (data) {
                console.log(data)
                var tr=""
                if (data.searchNotFound && data.phones!="") {
                    let timerInterval;
                    Swal.fire({
                        title: "Searching Products!!",
                        html: "I will close in <b></b> milliseconds.",
                        timer: 2000,
                        timerProgressBar: true,
                        didOpen: () => {
                            Swal.showLoading();
                            const timer = Swal.getPopup().querySelector("b");
                            timerInterval = setInterval(() => {
                                timer.textContent = `${Swal.getTimerLeft()}`;
                            }, 100);
                        },
                        willClose: () => {
                            clearInterval(timerInterval);
                        }
                    }).then((result) => {
                        /* Read more about handling dismissals below */
                        if (result.dismiss === Swal.DismissReason.timer) {
                            $.each(data.phones, function (ind, val) {
                                console.log(val)
                                tr += `
                            <tr class="align-middle">
                            <td>${ind + 1}</td>
                            <td>${val["proName"]}</td>
                            <td>${val["discount"]}&#x25;</td>
                            <td>${val["color"]}</td>
                            <td>${val["ram"]} G</td>
                            <td>${val["rom"]} GB</td>
                            <td>${val["camera"]} MP</td>
                            <td>${val["bettery"]} mAh</td>
                            <td>${val["display"]}  inches</td>
                            <td class="bg-danger bg-opacity-25">${val["createAt"]}</td>
                            <td class="bg-primary bg-opacity-25">${val["updateAt"]}</td>
                            <td>
                                <button class="btn btn-success"  data-bs-toggle="modal" data-bs-target="#edite_detail"  onclick="editePhone(${val['id']})"><i class="bi bi-pencil-square"></i> ${val['id']}</button>
                                <button class="btn btn-danger" onclick="deleteInfo(${val['id']})"><i class="bi bi-trash-fill"></i></button>
                            </td>
                            </tr>
                                  `
                                $("#tbl_detailPhone").html(tr)
                            })
                        }
                    });
                }
                if (data.phones == "") {
                    Swal.fire({
                        title: "NotFound!",
                        text: "Not has Products!!",
                        imageUrl: "https://i.pinimg.com/originals/a3/97/c3/a397c31c461068e6b1bebf79df999109.gif",
                        imageWidth: 400,
                        imageHeight: 200,
                    }); setTimeout(() => window.location.reload(), 3800)
                }
            }
        })


    })



    //
    $("#breviewBrand").change(function () {
        var frm_img = new FormData($("#frm_add_brand")[0]);
        $.ajax({
            url: "/Brand/GetImage",
            data: frm_img,
            dataType: "JSON",
            type: "POST",
            processData: false,
            contentType: false,
            success: function (data) {
                console.log(data)
                $("#ImgNew").val(data.img_name)
                $("#img-brand").attr("src", `TempImages/${data.img_name}`)
                $(".imgOldPreview").attr("src", `TempImages/${data.img_name}`)
            }
        })
    })






})


//const show_side = () => {
//    $("#side-show").css("width","300px")
//    console.log(side)
//}

    //$("#btn-show").click(function () {
    //    $("#side-show").css("width", "300px")
    //    $("#side-show>ul>li>h4").css("width","70%")
    //})
    $("#img-averta").attr("src","https://i.pinimg.com/736x/4c/eb/c7/4cebc7ad4db4710514aceffaada63550.jpg")


//search

//search phone detail

function search() {
    let frm = $("#search").serialize()
    $.ajax({
        url: "/PhoneDetail/Search",
        data: frm,
        type: "POST",
        dataType: "JSON",
        success: function (data) {
            var tr = ""
            if (data.searchNotFound) {
                let timerInterval;
                Swal.fire({
                    title: "Searching Products!!",
                    html: "I will close in <b></b> milliseconds.",
                    timer: 2000,
                    timerProgressBar: true,
                    didOpen: () => {
                        Swal.showLoading();
                        const timer = Swal.getPopup().querySelector("b");
                        timerInterval = setInterval(() => {
                            timer.textContent = `${Swal.getTimerLeft()}`;
                        }, 100);
                    },
                    willClose: () => {
                        clearInterval(timerInterval);
                    }
                }).then((result) => {
                    /* Read more about handling dismissals below */
                    if (result.dismiss === Swal.DismissReason.timer) {
                        $.each(data.search, function (ind, val) {
                            tr += `
                            <tr class="align-middle">
                            <td>${ind + 1}</td>
                            <td>${val["proName"]}</td>
                            <td>${val["discount"]}&#x25;</td>
                            <td>${val["color"]}</td>
                            <td>${val["ram"]} G</td>
                            <td>${val["rom"]} GB</td>
                            <td>${val["camera"]} MP</td>
                            <td>${val["bettery"]} mAh</td>
                            <td>${val["display"]}  inches</td>
                            <td class="bg-danger bg-opacity-25">${val["createAt"]}</td>
                            <td class="bg-primary bg-opacity-25">${val["updateAt"]}</td>
                            <td>
                                <button class="btn btn-success"  data-bs-toggle="modal" data-bs-target="#edite_detail"  onclick="editePhone(${val['id']})"><i class="bi bi-pencil-square"></i> ${val['id']}</button>
                                <button class="btn btn-danger" onclick="deleteInfo(${val['id']})"><i class="bi bi-trash-fill"></i></button>
                            </td>
                            </tr>
                                  `
                            $("#tbl_detailPhone").html(tr)
                        })
                    }
                });
            } else {
               getAllDetail()
            }
            if (data.search == "") {
                Swal.fire({
                    title: "NotFound!",
                    text: "Not has Products!!",
                    imageUrl: "https://i.pinimg.com/originals/a3/97/c3/a397c31c461068e6b1bebf79df999109.gif",
                    imageWidth: 400,
                    imageHeight: 200,
                }); setTimeout(() => window.location.reload(),3800)
            }
        }
    })
}





//broducts
function previewImage() {

    var frm_data = new FormData($("#frm_products")[0]);
    $.ajax({
        url: "/Product/PreviewImg",
        data: frm_data,
        type: "POST",
        contentType: false,
        processData: false,
        dataType: "JSON",
        success: function (data) {
            console.log(data)
            $(".img_name").val(data)
            $("#img_name").val(data)
            $(".img-preview").attr("src", `/Tempimages/${data}`)
        }
    })
}
//brands



//Brand script

function getAllBrand() {
    $.ajax({
        url: "/Brand/GetAllBrand",
        type: "GET",
        dataType: "JSON",
        success: function (data) {
            var tr=""
            $.each(data.brands, function (ind, val) {
                console.log(val)
                tr+=`
                <tr class="align-middle">
                <td>${ind+1}</td>
                <td>${val['brand_name']}</td>
                <td><img style="height:80px" src="/images/Brands/${val['img_name']}" /></td>
                <td>${val['brand_description']}</td>
                <td>
                <button class="btn-primary btn" onclick="FindBrand(${val["id"]})" data-bs-toggle="modal" data-bs-target="#update_brand"><i class="bi bi-pencil-square"></i></button>
                <button class="btn btn-danger"><i class="bi bi-trash"></i></button>
                
                </td>
                </tr>
                `
                $("#tbl_brand").html(tr)

            })
        }
    })
}
getAllBrand();


function insertBrand() {
    //var objTitle_brand = {
    //    Brand_name: $("#Brand_name").val(),
    //    Brand_description: $("#Brand_description").val()
    //}

    var frm_img = $("#frm_add_brand").serialize();

    $.ajax({
        url: "/Brand/Create",
        type: "POST",
        data: frm_img,
        dataType: "JSON",
        success: function (data) {
            console.log(data)
        }
    })}


function FindBrand(id) {
    $.ajax({
        url: "/Brand/Edite",
        data: {id:id} ,
        method: "GET",
        dataType:"JSON",
        success: function (data) {
            console.log(data)
            console.log(data["brand_name"])
            $("#Name").val(data["brand_name"])
            $("#Desc").val(data["brand_description"])
            $("#Id").val(data["id"])
            $("#imgOld").val(data["img_name"])
            $("#create").val(data["create_at"]);
            $(".imgOldPreview").attr('src', `images/Brands/${data["img_name"]}`)
        }
    })
}
function update() {
    let frm = new FormData($("#frm_update")[0])
 
    $.ajax({
        url: "/Brand/Edite",
        data: frm,
        type: "POST",
        dataType: "JSON",
        contentType: false,
        processData: false,
        success: function (data) {
            console.log(data)
            if (data.isUpdate == true) {
                Swal.fire({
                    position: "center",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500
                });
                setTimeout(function () {
                    window.location.reload()
                }, 1300)
            } else if (data.isUpdate == false) {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                 
                });
            } else {
                Swal.fire({
                    icon: "warning",
                    title: "Something went wrong!",
                    text: "Check Field Again!!",

                });
            }
        }
    })
}

function deleteBrand(id) {
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
            $.ajax({
                url: "/Brand/Delete",
                data: { id: id },
                type: "POST",
                dataType: "JSON",
                success: function (data) {
                    if (data.status == true) {
                        Swal.fire({
                            title: "Deleted!",
                            text: "Your file has been deleted.",
                            icon: "success"
                        }, setTimeout(function () { window.location.reload() },1500));
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: "Can't Delete Brand!!",
                            text: "This Brand is use can't delete it!!",
                        });
                    }
                }
            })
        }
    });
   
}




//category script

function insert_cate() {
    var frm_data = $("#frm_add_cate").serialize();
    console.log(frm_data)
    $.ajax({
        url: "/Categories/Create",
        data: frm_data,
        type: "POST",
        dataType: "JSON",
        success: function (data) {
            if (data.status == true) {
                Swal.fire({
                    position: "center",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500
                }); setTimeout(function () { window.location.reload() }, 1300)
            } else {
                Swal.fire({
                    icon: "warning",
                    title: "Oops...",
                    text: "Dubplicate Data!!",
                  
                });
            }
        }



    })
}


function edite_cate(id) {
    $.ajax({
        url: "/Categories/Edite",
        data: { id: id },
        dataType: "JSON",
        type: "GET",
        success: function (data) {
          
            $("#up_cate").val(data.categoriesName)
            $("#id").val(data.id)
        }
    })
}

function update_cate() {
    var frm_up_cate = $("#update_cate").serialize();
    $.ajax({
        url: "/Categories/Edite",
        data: frm_up_cate,
        dataType: "JSON",
        type: "POST",
        success: function (data) {
       
            if (data.status) {
                Swal.fire({
                    position: "center",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500
                }); setTimeout(function () { window.location.reload() }, 1300)
            } else {
                $("#up_cate").focus();
                Swal.fire({
                    icon: "warning",
                    title: "Oops...",
                    text: "Empty Category name!!!",
                });   
            }
        }
    })
}

function delete_cate(id) {
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

            $.ajax({
                url: "/Categories/Delete",
                data: { id: id },
                dataType: "JSON",
                type: "POST",
                success: function (data) {
                    if (data.status) {
                        Swal.fire({
                            title: "Deleted!",
                            text: "Your file has been deleted.",
                            icon: "success"
                        });
                        setTimeout(function () { window.location.reload() },1300)
                    } else {
                        Swal.fire({
                            icon: "warning",
                            title: "Oops...",
                            text: "Something went wrong!",
                        });
                    }
                }
            })
        }
    });
}




function getProducts() {

    $.ajax({
        url: "/Product/getProducts",
        type: "GET",
        dataType: "JSON",
        success: function (data) {
          
            var tr = ""
            $.each(data.data, function (ind, val) {
                tr += `
                <tr class="align-middle">
                <td>
                    ${val['id']}
                </td>
                <td>
                    ${val['pro_name']}
                </td>  <td>
                    ${val['invetory']}
                </td>  
                <td>
                    ${val['prices'].toFixed(2)}
                </td>     
                <td>
                    ${val['cate_name']}
                </td>
                  <td>
                    ${val['brand_name']}
                </td>
                  <td>
                    <img style="width:60px;height:80px;object-fit:cover" src="/images/Products/${val['imgName']}"/>
                </td>
                   <td>
                  <button class="btn btn-success" onclick="editePro(${val["id"]})" data-bs-toggle="modal" data-bs-target="#edite_pro"><i class="bi bi-pencil-square"></i></button>
                  <button class="btn btn-danger" onclick="deletePro(${val["id"]})"> 
                  <i class="bi bi-trash-fill"></i>
                  </button>
                  
                  </td>
                </tr>
                `
                $("#tbl_products").html(tr)
            })
        }
    })
}
getProducts()



function inserProducts() {
    var frm_data = new FormData($("#frm_products")[0]);
    $.ajax({
        url: "/Product/Create",
        type: "POST",
        data: frm_data,
        dataType: "JSON",
        contentType: false,
        processData: false,
        success: function (data) {
          
            if (data.status) {
                Swal.fire({
                    position: "center",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500
                });
                setTimeout(() => window.location.reload(),1300)
            } else {
                Swal.fire({
                    icon: "warning",
                    title: "Something went wrong!",
                    text: "Check data again !",

                });
            }
        }
    })
}

function editePro(id) {
    $.ajax({
        url: "/Product/FindProduct",
        data: { id: id },
        type: "GET",
        dataType: "JSON",
        success: function (data) {
          
            $("#pro_name").val(data.data['pro_name'])
            $("#qty").val(data.data['qty'])
            $("#price").val(data.data['prices'])
            $("#cate").prepend(`<option hidden selected value="${data.data['cate_id']}">${data.data["cate_name"]}</option>`)
            $("#brand").prepend(`<option hidden selected value="${data.data['brand_id']}">${data.data["brand_name"]}</option>`)
            $("#qty").prepend(`<option hidden selected value="${data.data['invetory']}">${data.data["invetoryCount"]}</option>`)
            $(".img-preview").attr("src", `/images/products/${data.data['imgName']}`)
            $("#img_old").val(data.data["imgName"])
            $("#code").val(data.data["productsCode"])
            $("#idUp").val(data.data['id'])
        }
    })
}
function updatePro() {
    var frm_up_pro = $("#update_pro").serialize();
    console.log(frm_up_pro)
    $.ajax({
        url: "/Product/Update",
        data: frm_up_pro,
        type: "POST",
        dataType: "JSON",
        success: function (data) {
            if (data.status) {
                Swal.fire({
                    position: "center",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500
                }); setTimeout(() => window.location.reload(),1300)
            } else {
                Swal.fire({
                    icon: "warning",
                    title: "Something went wrong!",
                    text: "Check data again !",
                });
            }
        }
        })

}



function deletePro(id) {
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
            $.ajax({
                url: "/Product/Delete",
                data: { id: id },
                type: "POST",
                dataType: "JSON",
                success: function (data) {
                  
                    if (data.isDelete) {
                        Swal.fire({
                            title: "Deleted!",
                            text: "Your file has been deleted.",
                            icon: "success"
                        }); setTimeout(() => window.location.reload(),1300)
                    }
                }
            })
        }
    });
}


//Detail Phone


function getAllDetail() {
    $.ajax({
        url: "/PhoneDetail/GetAll",
        type: "GET",
        dataType: "JSON",
        success: function (data) {
            var tr=""
            $.each(data, function (ind, val) {
                console.log(val)
                tr += `
                <tr class="align-middle">
                <td>${ind+1}</td>
                <td>${val["proName"]}</td>
                <td>${val["discount"]}&#x25;</td>
                <td>${val["color"]}</td>
                <td>${val["ram"]} G</td>
                <td>${val["rom"]} GB</td>
                <td>${val["camera"]} MP</td>
                <td>${val["bettery"]} mAh</td>
                <td>${val["display"]}  inches</td>
                <td class="bg-danger bg-opacity-25">${val["createAt"]}</td>
                <td class="bg-primary bg-opacity-25">${val["updateAt"]}</td>
                <td>
                    <button class="btn btn-success"  data-bs-toggle="modal" data-bs-target="#edite_detail"  onclick="editePhone(${val['id']})"><i class="bi bi-pencil-square"></i> ${val['id']}</button>
                    <button class="btn btn-danger" onclick="deleteInfo(${val['id']})"><i class="bi bi-trash-fill"></i></button>
                </td>
                </tr>
                
                
                
                `
                $("#tbl_detailPhone").html(tr)

            })
        }
    })
}
getAllDetail();


function addInfoPhone() {
    var frm_data = $("#frm_info").serialize();
 
    $.ajax({
        type: "POST",
        url: "/PhoneDetail/AddInfo",
        data: frm_data,
        dataType: "JSON",
        success: function (data) {
            if (data.insert) {
                Swal.fire({
                    position: "center",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500
                }); setTimeout(() => window.location.reload(),1300)
            } else {
                Swal.fire({
                    position: "center",
                    icon: "warning",
                    title: "Check your information again!!!",
                    showConfirmButton: false,
                   
                });
            }
        }
    })
}



function editePhone(id) {
    $.ajax({
        url: "/PhoneDetail/Edite",
        data: { id: id },
        dataType: "JSON",
        type: "POST",
        success: function (data) {
            console.log(data)
            $("#id").val(data.findId["id"]);
            $("#create").val(data.findId["createAt"]);
            $("#Ram").val(data.findId['ram'])
            $("#Rom").val(data.findId['rom'])
            $("#Camera").val(data.findId['camera'])
            $("#Display").val(data.findId['display'])
            $("#Bettery").val(data.findId['bettery'])
            $("#ProId").prepend(`<option value="${data.findId["proId"]}" selected hidden>${data.findId['proName']}</option>`)
            $("#Color").prepend(`<option value="${data.findId["color"]}" selected hidden>${data.findId['color']}</option>`)
            $("#Dis").prepend(`<option value="${data.findId["discount"]}" selected hidden>${data.findId['discount']}</option>`)
        }
    })
}

function updateInfoPhone() {
    var frm_data = $("#frm_upinfo").serialize();
    $.ajax({
        type: "POST",
        url: "/PhoneDetail/Update",
        data: frm_data,
        dataType: "JSON",
        success: function (data) {
            if (data.isUpdate) {
                Swal.fire({
                    position: "center",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500
                }); setTimeout(() => window.location.reload(), 1300)
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Something went wrong!",
                    text: "Please check again!!!",
                
                });
            }
        }
    })
}

    function deleteInfo(id) {
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
                $.ajax({
                    type: "POST",
                    url: "/PhoneDetail/Delete",
                    data: { id: id },
                    dataType: "JSON",
                    success: function (data) {
                        if (data.isDelete) {
                            Swal.fire({
                                title: "Deleted!",
                                text: "Your file has been deleted.",
                                icon: "success"
                            }); setTimeout(() => window.location.reload(), 1300)
                        }
                    }
                })
            }
        });
}

       // script computer
function insertPc() {
    let frm = $("#frmInsertPc").serialize();
    $.ajax({
        url: "/DetailComputer/Create",
        data: frm,
        dataType: "JSON",
        type: "POST",
        success: function (data) {
            if (data.computer) {
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500
                }); setTimeout(() => window.location.reload(), 1300)
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",

                });
            }
        }

    })
}

function getPc() {
    $.ajax({
        url: "/DetailComputer/getPc",
        type: "GET",
        dataType: "JSON",
        success: function (data) {
                console.log(data)
            var tr=""
            $.each(data, function (ind, val) {
                console.log(val["pcId"])
                tr += `
                <tr class="align-middle">
                <td>${ind + 1}</td>
                <td>${val["proName"]}</td>
                <td>${val["dis"]}&#x25;</td>
                <td>${val["color"]}</td>
                <td>${val["ram"]} G</td>
                <td>${val["rom"]} GB</td>
                <td>${val["webcam"]} MP</td>
                <td>${val["battery"]} mAh</td>
                <td>${val["display"]}  inches</td>
                <td class="">${val["processor"]}</td>
                <td class="">${val["interfacePort"]}</td>
                <td class="d-flex gap-3">
                    <button class="btn  btn-success"  data-bs-toggle="modal" data-bs-target="#edite_pc"  onclick="editePc(${val["pcId"]})"><i class="bi bi-pencil-square"></i> </button>
                    <button class="btn btn-danger" onclick="deletePc(${val['pcId']})"><i class="bi bi-trash-fill"></i></button>
                </td>
                </tr>
                `
                $("#tbl_detailPc").html(tr)

            })
        }

    })
}
getPc()


function editePc(id) {
    $.ajax({
        url: "/DetailComputer/Edite",
        type: "GET",
        data: {id:id},
        dataType: "JSON",
        success: function (data) {
            $("#pcId").val(data.computer["pcId"]);
            $("#ProId").val(data.computer["proId"]);
            $("#Dis").val(data.computer["dis"]);
            $("#Color").val(data.computer["color"]);
            $("#Ram").val(data.computer["ram"]);
            $("#Rom").val(data.computer["rom"]);
            $("#Processor").val(data.computer["processor"]);
            $("#Display").val(data.computer["display"]);
            $("#Graphic").val(data.computer["graphic"]);
            $("#Keyboard").val(data.computer["keyboard"]);
            $("#Webcam").val(data.computer["webcam"]);
            $("#Connectivity").val(data.computer["connectivity"]);
            $("#InterfacePort").val(data.computer["interfacePort"]);
            $("#Speaker").val(data.computer["speaker"]);
            $("#Battery").val(data.computer["battery"]);
            $("#Weight").val(data.computer["weight"]);
            $("#Os").val(data.computer["os"]);
            $("#Code").val(data.computer["code"]);
            $("#Create").val(data.computer["createAt"]);
            $("#UpdateAt").val(data.computer["updateAt"]);
        }
    })
}


function updateDetailPc() {
    let frm = $("#frmUpdatePc").serialize();
    $.ajax({
        url: "/DetailComputer/Update",
        data: frm,
        dataType: "JSON",
        type: "POST",
        success: function (data) {
            if (data.isUpdate) {
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500
                }); setTimeout(() => window.location.reload(),1300)
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                   
                });
            }
            
        }
    })
}

function deletePc(id) {
    $.ajax({
        url: "/DetailComputer/Delete",
        data: {id:id},
        dataType: "JSON",
        type: "POST",
        success: function (data) {

            if (data.isDelete) {
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500
                }); setTimeout(() => window.location.reload(), 1300)
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                    
                });
            }
        }
    })
}