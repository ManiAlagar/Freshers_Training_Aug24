function delFunction(id) {
    let empId = parseInt(id);
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
                url: "/Employee/Delete/" + empId,
                contentType: "application/json;charset=UTF-8",
                success: function () {
                    setTimeout(myTimeout, 1000);
                    Swal.fire({
                        text: "Deleted", 
                        icon: "success",
                        showConfirmButton: false,
                        timer: 1500
                    });
                },
                error: function () {  
                    //alert("Error while deleting data");
                    Swal.fire({
                        text: "Error",
                        icon: "danger",
                        showConfirmButton: false,
                        timer: 1500
                    });
                }
            })
            
        }
    })
}

function myTimeout() {
    location.reload();
}



function delFuncDept(id) {
    let Id = parseInt(id);
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
                url: "/Department/Delete/" + Id,
                contentType: "application/json;charset=UTF-8",
                dataType: 'json',
                success: function (response) {
                    console.log(response);
                    setTimeout(myTimeout, 1000);

                    Swal.fire({
                        text: "Deleted",
                        icon: "success",
                        showConfirmButton: false,
                        timer: 1500
                    });
                },
                error: function () {
                   
                    Swal.fire({
                        text: "Error",
                        icon: "danger",
                        showConfirmButton: false,
                        timer: 1500
                    });
                }
            })

        }
    })
}