

$("#dpn").on("change", function () {
    var y = $("#dpn").val();

    $("#ct").click(function () {
        if (y == "2") {


            window.location.href = '/Org_Details/Create';


        }


        else {
            window.location.href = '/Indv_Donor/Create';

        }
    }
    );
});
    

