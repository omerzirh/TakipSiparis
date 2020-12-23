function Veriler() {
    var url = '/Orders/Refresh';
    $('table').html("");
    var thead = '  <thead style="background-color:aliceblue"> <tr> <th>ID</th> <th>Category</th> <th>Brand</th><th>Product Name</th><th>Amount</th><th>Price</th><th>Order Date</th><th>Payment Method</th><th>Estimated Arrival</th><th>Attachment</th><th>Termin Date</th><th>User</th><th>Note</th><th>Decision</th><th> </th></tr></thead>';
    $('table').append(thead);
    $.getJSON(url, function (data) {
        for (var item in data.Result) {
            var update = ' <td><input type="submit" name="btn" value="Update Termin" class="btn btn-success update" data-id=' + data.Result[item].id + ' /> ';
            var values = '<tbody><tr><td>' + data.Result[item].ID + '</td><td>' + data.Result[item].CategoryID + '</td><td>' + data.Result[item].BrandID + '</td><td>' + data.Result[item].Amount + '</td><td>' + data.Result[item].Price + '</td><td>' + data.Result[item].OrderDate + '</td><td>' + data.Result[item].PayMethID + '</td><td>' + data.Result[item].EstArrival + '</td><td>' + data.Result[item].AtchID + '</td><td>' + data.Result[item].TerminDate + '</td><td>' + data.Result[item].UserName + '</td><td>' + data.Result[item].Note + '</td><td>' + data.Result[item].DecisionID + '</td><td>' + update + '</td></tr></tbody>';
            $('table').append(values);
        }
    })
}


$(document).on("click", ".update", async function () {
    var id = $(this).attr('data-id');
    $.ajax({
        url: '/Orders/UpdateTermin',
        type: 'Post',
        dataType: 'json',
        data: { 'id': id },
        success: async function (data) {
            const { value: orderTable } = await Swal.fire({
                title: 'Update Termin',
                showCancelButton: true,
                cancelButtonColor: '#d43f3a',
                cancelButtonText: 'Iptal',
                confirmButtonText: 'Confirm',
                confirmButtonColor: '#337ab7',
                html:
                    '<input id="ProductName" value=' + data.Result.ProductName + 'placeholder="Ad gir" class="swal2-input">' +
                    '<input id="CategoryID" value=' + data.Result.CategoryID + 'placeholder="Ad gir" class="swal2-input">' +
                    '<input id="BrandID" value=' + data.Result.BrandID + 'placeholder="Ad gir" class="swal2-input">' +
                    '<input id="Amount" value=' + data.Result.Amount + 'placeholder="Ad gir" class="swal2-input">' +
                    '<input id="Price" value=' + data.Result.Price + 'placeholder="Ad gir" class="swal2-input">' +
                    '<input id="OrderDate" value=' + data.Result.OrderDate + 'placeholder="Ad gir" class="swal2-input">' +
                    '<input id="PayMethID" value=' + data.Result.PayMethID + 'placeholder="Ad gir" class="swal2-input">' +
                    '<input id="PaymentDate" value=' + data.Result.PaymentDate + 'placeholder="Ad gir" class="swal2-input">' +
                    '<input id="EstArrival" value=' + data.Result.EstArrival + 'placeholder="Ad gir" class="swal2-input">' +
                    '<input id="AtchID" value=' + data.Result.AtchID + 'placeholder="Ad gir" class="swal2-input">' +
                    '<input id="UserName" value=' + data.Result.UserName + 'placeholder="Ad gir" class="swal2-input">' +
                    '<input id="Note" value=' + data.Result.Note + 'placeholder="Ad gir" class="swal2-input">' +
                    '<input id="DecisionID" value=' + data.Result.DecisionID + 'placeholder="Ad gir" class="swal2-input">',
                focusConfirm: false,
                preConfirm: () => {
                    return [
                        document.getElementById('ProductName').value,
                        document.getElementById('CategoryID').value,
                        document.getElementById('BrandID').value,
                        document.getElementById('Amount').value,
                        document.getElementById('Price').value,
                        document.getElementById('OrderDate').value,
                        document.getElementById('PayMethID').value,
                        document.getElementById('EstArrival').value,
                        document.getElementById('AtchID').value,
                        document.getElementById('UserName').value,
                        document.getElementById('Note').value,
                        document.getElementById('DecisionID').value,

                    ]
                }
                    

            })
            //$.ajax({
            //    url: '/Orders/UpdateTermin',
            //    type: 'Post',
            //    dataType: 'json',
            //    data: {
            //        id: id, ProductName: orderTable[0], CategoryID: orderTable[1], BrandID: orderTable[2], Amount: orderTable[3], Price: orderTable[4], OrderDate: orderTable[5], PayMethID: orderTable[6], EstArrival: orderTable[7], AtchID: orderTable[8], UserName: orderTable[9], Note: orderTable[10], DecisionID: orderTable[11]
            //    },
            //    success: function (incVal) {
            //        if (incVal == '1') {
            //            Swal.fire({
            //                type: 'success',
            //                title: 'Added!',
            //                text: 'Proccessed successfuly'
            //            })

            //        } else {
            //            Swal.fire({
            //                type: 'error',
            //                title: 'Couldnt Added!',
            //                text: 'An error occured!'
            //            })
            //        }
            //    }
            //});
        }
    });

});