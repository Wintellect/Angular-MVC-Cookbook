
$(function () {

    $("#dataGrid").jqGrid({
        url: '/api/people',
        datatype: 'json',
        mtype: 'GET',
        colModel: [
            { name: "lastName", width: 200, label: 'Last' },
            { name: "firstName", width: 200, label: 'First' },
            { name: "middleName", width: 100, label: 'Middle' },
            { name: "suffix", width: 100, label: 'Suffix' },
            { name: "title", width: 100, label: 'Title' }
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
