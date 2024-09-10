$(document).ready(function () {
  $(".checkAll").click(function () {
    $('.firstSet  :checkbox').prop("checked", true)
  });
  $(".unCheckAll").click(function () {
    $('.firstSet :checkbox').prop("checked", false)
  });
  $(".alterCheck").click(function () {
    $('.firstSet :checkbox').each(function () {
      this.checked = !this.checked
    });
  });
});