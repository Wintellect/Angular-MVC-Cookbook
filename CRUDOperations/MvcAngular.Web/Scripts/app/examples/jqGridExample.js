
$(function () {

    $("#dataGrid").jqGrid({
        url: '/api/people',
        datatype: 'json',
        mtype: 'GET',
        colNames: [
            "Last",
            "First",
            "Middle",
            "Suffix",
            "Title"
        ],
        colModel: [
            { name: "lastName", width: 200 },
            { name: "firstName", width: 200 },
            { name: "middleName", width: 100 },
            { name: "suffix", width: 100 },
            { name: "title", width: 100 }
        ],
        pager: "#dataGridPager",
        autowidth: true,
        height: 'auto',
        rowNum: 20,
        sortname: "lastName",
        sortorder: "asc",
        viewrecords: true,
        gridview: true,
        autoencode: true,
        caption: "Example jQuery Grid",
        gridComplete: function(e) {
            console.log('Grid Complete');
            console.log(e);
        },
        loadComplete: function (e) {
            console.log('Load Complete');
            console.log(e);
        }
    });

});
