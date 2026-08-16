$('#tblData').DataTable({
    ajax: '/product/getallproducts', 
        columns: [
            { data: 'title',width:"25%"},
            { data: 'isbn', width: "15%" },
            { data: 'listPrice', width: "10%", "render": function (data) { return '$' + data.toFixed(2); } },
            { data: 'author', width: "15%" },
            {
                data: 'category.name', width: "10%",
                "render": function (data)
                {
                    return '<span class="badge bg-secondary">' + data + '</span>'
                }
            },
            { defaultContent:'',width:"25%"}
    ]
});