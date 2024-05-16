class MyTable {
    constructor(tableId) {
        this.tableId = tableId;
        this.initDropdown();
        this.initCheckbox();
        //this.initTableHeader();
    }

    showTableName() {
        alert(this.tableId + " showtable");
    }

    initDropdown() {
        $("#" + this.tableId + "Dropdown").on('change', function () {
            window.location = "/?PageSize=" + this.value;
        })
    }

    initCheckbox() {
        var tableId = this.tableId;
        $("#" + this.tableId + "HeaderCheckbox").on('change', function () {
            var isChecked = $(this).is(":checked");
            var box = $("#" + tableId + " ." + tableId + "Class");
            box.prop("checked", isChecked);
        })
    }

    initTableHeader() {
        var tableId = this.tableId;
        //$("#" + this.tableId + " thead tr th").on('click', function () {
        //    alert("test header");
        //})
    }
}