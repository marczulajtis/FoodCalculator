
//$('.mealDetailsTable').find('tr[data-id]').on('click', function () {
//    $('#mealDetails').modal('show');

//    var getIdFromRow = $(event.target).closest('tr').data('id');

//    $("#mealID").val(getIdFromRow);
//});

//$(document).on("click", ".modalLink", function () {
//    var passedID = $(this).data('id');
//    $(".modal-body .hiddenid").val(passedID);    
//});

$(document).ready(function () {
    $("#result").dialog({
        autoOpen: false,
        title: 'Meal Details',
        width: 500,
        height: 'auto',
        modal: true
    });
});
function openPopup() {
    $("#result").dialog("open");
}