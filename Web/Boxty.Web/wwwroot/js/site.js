function openAccordion(orderId) {
    var button = document.getElementById("b" + orderId);
    var div = document.getElementById("d" + orderId);

    button.classList.toggle("active");

    var panel = div;
    if (panel.style.display === "block") {
        panel.style.display = "none";
    } else {
        panel.style.display = "block";
    }
}

function initDriverOrderItems() {
    $.ajax({
        type: "GET",
        url: "/api/Driver",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('.table > tr').slice(0).remove();
            $.each(data, function (i, order) {
                var button = "<button class='accordion' onclick='openAccordion(" + order.id + ")' id='b" + order.id + "'>" + "OrderId: " + order.id + ", Destination: " + order.destination + "</button>";
                $('.table').append(button);
                $('.table').append("<div class='panel' id='d" + order.id + "'>");
                $.each(order.orderItems, function (j, orderItem) {
                    var row = "<li>Product: " + orderItem.product.name + ", Amount: " + orderItem.amount + ", Status: " + orderItem.status + "</li>";

                    $("#d" + order.id).append(row);
                })
                $("#d" + order.id).append("</div>");
                var btnDone = '<button onclick="markAsCompletedAsDriver(' + order.id + ')" class="btn btn-success">Mark As Done</button>'
                $("#d" + order.id).append(btnDone);
            });
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function markAsCompletedAsDriver(id) {
    $.ajax({
        type: "POST",
        url: "/api/Driver/MarkAsCompleted?orderId=" + id,
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            initDriverOrderItems();
        },
        failure: function (xhr, status, error) {

        },
        error: function (xhr, status, thrownError) {
            alert('Error ' + thrownError + " " + xhr.status);
        }

    })
}

function markAsDoneOrderItem(id) {
    $.ajax({
        type: "POST",
        url: "/api/OrderItem/MarkAsDone?orderItemId=" + id,
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            initKitchenOrderItems();
        },
        failure: function (xhr, status, error) {

        },
        error: function (xhr, status, thrownError) {
            alert('Error ' + thrownError + " " + xhr.status);
        }

    })
}

function markAsServed(id) {
    $.ajax({
        type: "POST",
        url: "/api/OrderItem/MarkAsServed?orderItemId=" + id,
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            initPosLineWaiter();
        },
        failure: function (xhr, status, error) {

        },
        error: function (xhr, status, thrownError) {
            alert('Error ' + thrownError + " " + xhr.status);
        }

    })
}

function initKitchenOrderItems() {
    $.ajax({
        type: "GET",
        url: "/api/OrderItem",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('.table > tr').slice(0).remove();
            $.each(data, function (i, item) {
                var btnComment = '<button onclick="markAsDoneOrderItem(\'' + item.id + '\')" class="btn btn-success">Mark As Done</button>';
                var rows = "<tr name='orderItem'>" +
                    "<td>" + item.product.name + "</td>" +
                    "<td>" + item.comment + "</td>" +
                    "<td>" + item.createdOn + "</td>" +
                    "<td>" + item.modifiedOn + "</td>" +
                    "<td>" + btnComment + "</td>"
                "</tr>";
                $('.table').append(rows);
            });
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function completeOrder() {
    var tableId = document.getElementById("tableList").value;

    $.ajax({
        type: "POST",
        url: "/api/Order/CompleteOrder?tableId=" + tableId,
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            initPosLineWaiter();
            initTablesList();
        },
        failure: function (xhr, status, error) {

        },
        error: function (xhr, status, thrownError) {
            alert('Error ' + thrownError + " " + xhr.status);
        }

    })
}

function sumTotalWaiter() {
    var sum = 0;
    $.each(document.getElementsByName("subtotal"), function (i, item) {
        sum += Number(item.textContent);
    })
    document.getElementById("total").innerText = sum;
}

function showTableItemCommentBox(index) {
    $("#myModal").modal();
    document.getElementById("itemIndexInput").value = index;
    document.getElementById("tableIdInput").value = document.getElementById("tableList").value;
}

function addComment(id) {
    var tableId = document.getElementById("tableList").value;
    var index = Number(document.getElementById("itemIndexInput").value);
    var comment = document.getElementById("commentInput").value;

    var data = { "TableId": Number(tableId), "ItemIndex": Number(index), "Comment": comment };

    $.ajax({
        type: "POST",
        url: "/api/TableItem/Comment",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            initPosLinePendingItems();
        },

        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert('Error ' + data.responsetext);
        }
    });
}

function openCategory(event, categoryName) {
    var i, tabcontent, tablinks;

    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    document.getElementById(categoryName).style.display = "block";
    event.currentTarget.className += " active";
}

function loadCategoriesButtons() {
    $.ajax({
        type: "GET",
        url: "/api/Product",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data, function (i, item) {
                var button = '<button type="button" class="btn btn-primary" onclick="posLineInsertWaiter(' + item.id + ')">' + item.name + '</button>'
                var buttonElement = document.createElement("button");
                buttonElement.type = "button";
                buttonElement.className = "btn btn-primary";
                buttonElement.onclick = function () {
                    posLineInsertWaiter(item.id);
                }
                buttonElement.innerHTML = item.name;
                document.getElementById(item.categoryName).append(buttonElement)
            })
        },

        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function loadCategories() {
    $.ajax({
        type: "GET",
        url: "/api/Category",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data, function (i, item) {
                var rows = '<button class="tablinks" onclick="openCategory(event, \'' + item.name + '\')">' + item.name + '</button>';
                $('#categoriesTabs').append(rows);

                $('<div id=\'' + item.name + '\' class="tabcontent"></div>').appendTo('#productDiv');
            });
            loadCategoriesButtons();
        },

        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}


function sendItems() {
    var tableId = document.getElementById("tableList").value;

    $.ajax({
        type: "POST",
        url: "/api/TableItem/SendOrder?id=" + tableId,
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            initPosLineWaiter();
            initTablesList();
        },
        failure: function (xhr, status, error) {

        },
        error: function (xhr, status, thrownError) {
            alert('Error ' + thrownError + " " + xhr.status);
        }

    })
}

function initTablesList() {
    $.ajax({
        type: "GET",
        url: "/api/Table",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#tableList > option').slice(1).remove();
            $.each(data, function (i, item) {
                var rows = ""
                if (item.available == true) {
                    rows = "<option style='color:green' value=" + item.id + ">" + item.id + "</option>"
                }
                else {
                    rows = "<option style='color:red' value=" + item.id + ">" + item.id + "</option>"
                }
                $('#tableList').append(rows);
            });
        },

        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function initPosLineWaiter() {
    var tableId = Number(document.getElementById("tableList").value);
    if (isNaN(tableId)) {
        $('#posLine > tr').slice(0).remove();
    }
    else {
        $.ajax({
            type: "GET",
            url: "/api/TableItem/GetTableItems?tableId=" + tableId + "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#posLine > tr').slice(0).remove();
                var items = document.getElementsByClassName("table")[0].rows;

                $.each(data, function (i, item) {
                    var t = 0;
                    items = document.getElementsByClassName("table")[0].rows;
                    if (items.length > 1) {
                        for (var i = 1; i < items.length; i++) {
                            var temp = items[i].getElementsByTagName("td");

                            if (temp[1].innerHTML == item.productName && temp[2].innerHTML == item.comment && temp[6].innerHTML == item.status) {
                                temp[4].innerText = Number(temp[4].innerText) + 1;
                                temp[5].innerText = Number(temp[4].innerText) * item.productPrice

                                temp[0].innerText = item.modifiedOn
                                t = 1;
                                break;
                            }
                        }
                    }
                    if (t == 0) {
                        var name = 'sentItem' + i++;
                        var button = "";
                        if (item.status == "completed") {
                            button = '<button onclick="markAsServed(\'' + item.id + '\')" class="btn btn-info">Mark As Served</button>'
                            name = 'completedItem' + i++;
                        }

                        var rows = "<tr name=\"" + name + "\">" +
                            "<td>" + item.modifiedOn + "</td>" +
                            "<td style='color:green 'id='name'>" + item.productName + "</td>" +
                            "<td name='comment'>" + item.comment + "</td> " +
                            "<td>" + item.productPrice + "</td>" +
                            "<td>1</td>" +
                            "<td name='subtotal'>" + item.productPrice + "</td>" +
                            "<td name='status'>" + item.status + "</td>" +
                            "<td>" + button + "</td>" +
                            "</tr>";
                        $('#posLine').append(rows);
                    }
                });
                initPosLinePendingItems();
            },

            failure: function (data) {
                alert(data.responseText);
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
}


function initPosLinePendingItems() {
    var tableId = document.getElementById("tableList").value;

    $.ajax({
        type: "GET",
        url: "/api/TableItem/GetPendingItems?tableId=" + tableId + "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("[name=pendingItem]").remove();
            var items = document.getElementsByClassName("table")[0].rows;

            for (var i = 1; i < items.length; i++) {
                var temp = items[i].getElementsByTagName("td");
                if (temp[6].innerText == "marked") {
                        temp[4].innerText = 0;
                }
            }
            $.each(data, function (i, item) {
                var t = 0;
                items = document.getElementsByClassName("table")[0].rows;
                if (items.length > 1) {


                    for (var j = 1; j < items.length; j++) {
                        var temp = items[j].getElementsByTagName("td");

                        if (temp[1].innerHTML == item.productName && temp[2].innerHTML == item.comment && temp[6].innerHTML == item.status) {
                            temp[4].innerText = Number(temp[4].innerText) + 1;
                            temp[5].innerText = Number(temp[4].innerText) * item.productPrice

                            temp[0].innerText = item.modifiedOn
                            t = 1;
                            break;
                        }
                    }
                }
                if (t == 0) {
                    var name = 'pendingItem' + i++;
                    var btnComment = '<button onclick="deleteLineWaiter(\'' + i + '\')" class="btn btn-danger">DELETE</button>' + '<button type="button" onclick="showTableItemCommentBox(' + i + ')" class="btn btn-info">Comment</button>';
                    var rows = "<tr name=\"" + name + "\">" +
                        "<td>" + item.modifiedOn + "</td>" +
                        "<td style='color:Red 'id='name'>" + item.productName + "</td>" +
                        "<td>" + item.comment + "</td>" +
                        "<td>" + item.productPrice + "</td>" +
                        "<td>1</td>" +
                        "<td name='subtotal'>" + item.productPrice + "</td>" +
                        "<td name='status'>" + item.status + "</td>" +
                        "<td>" + btnComment + "</td>"
                    "</tr>";
                    $('#posLine').append(rows);
                }

                sumTotalWaiter();
            });
        }
    })
}




function deleteLineWaiter(itemIndex) {
    var tableId = document.getElementById("tableList").value;

    var product = { "ItemIndex": Number(itemIndex), "TableId": Number(tableId) };

    $.ajax({
        type: "DELETE",
        url: "/api/TableItem",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(product),
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {

            initPosLinePendingItems();

        },
        failure: function (xhr, status, error) {
        },
        error: function (xhr, status, thrownError) {
            alert('Error ' + thrownError + " " + xhr.status);
        }
    })
}

function posLineInsertWaiter(productId) {
    var tableId = document.getElementById("tableList").value;
    var product = { "ProductId": Number(productId), "TableId": Number(tableId) };
    $.ajax({
        type: "POST",
        url: "/api/TableItem",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(product),
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            initPosLinePendingItems();
        },
        failure: function (xhr, status, error) {
        },
        error: function (xhr, status, thrownError) {
            alert('Error ' + thrownError + " " + xhr.status);
        }
    })
}
