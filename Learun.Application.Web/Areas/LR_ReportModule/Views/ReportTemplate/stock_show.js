//reloadpage();
$(document).ready(function () {
    reloadpage();
    setInterval(function () { reloadpage(); }, 500000);
});
function reloadpage() {
    $.ajax({
        url: 'GetStockInfo',
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            var tbody_content = "<table border:10px;><tr>";
            $.each(data, function (vi, item) {
                if (vi == 0) {
                    tbody_content += "<td>"
                        + "    <div class=\"row row-modal\" id=\"" + item.machine_name + "\" style=\"background-color:rgba(153, 204, 255, 1);height:200px;width:200px;text-align:center;\"> "
                        + "   <div style=\"height:30%;padding-top:50px;\"><h3 class=\"main-font\" style=\"margin-top:0;color:#000;font-weight:800;font-family:SimSun YaHei;\">"
                        + item.machine_name + "<br>" + "("+ item.group_name + ")套"+ "<br></h3></div>"
                        + " <div style=\"border:0px solid #000;width:50%;height:20px;float:left;text-align:center;\"><a href=\"stockunit_show?platform_name=" + escape(item.machine_name) + "\">单元</a></div><div style=\"border:0px solid #000;width:50%;height:20px;float:right;text-align:center;\"><a href=\"/LR_LGManager/stock_info_edit/Index?platform_name=" + escape(item.machine_name) + "\">零件</a></div></div></td>";
                }
                else {
                    if (vi % 4 == 0) {
                        tbody_content += "</tr><tr><td>"
                            + "    <div class=\"row row-modal\" id=\"" + item.machine_name + "\" style=\"background-color:rgba(153, 204, 255, 1);height:200px;width:200px;text-align:center;\"> "
                            + "   <div style=\"height:30%;padding-top:50px;\"><h3 class=\"main-font\" style=\"margin-top:0;color:#000;font-weight:800;font-family:SimSun YaHei;\">"
                            + item.machine_name + "<br>" + "("+item.group_name +")套"+ "<br></h3></div>"
                            + " <div style =\"border:0px solid #000;width:50%;height:20px;float:left;text-align:center;\"><a href=\"stockunit_show?platform_name=" + escape(item.machine_name) + "\">单元</a></div><div style=\"border:0px solid #000;width:50%;height:20px;float:right;text-align:center;\"><a href=\"/LR_LGManager/stock_info_edit/Index?platform_name=" + escape(item.machine_name) + "\">零件</a></div></div></td>";
                    }
                    else {
                        tbody_content += "<td>" 
                            + "    <div class=\"row row-modal\" id=\"" + item.machine_name + "\" style=\"background-color:rgba(153, 204, 255, 1);height:200px;width:200px;text-align:center;\"> "
                            + "   <div style=\"height:30%;padding-top:50px;\"><h3 class=\"main-font\" style=\"margin-top:0;color:#000;font-weight:800;font-family:SimSun YaHei;\">"
                            + item.machine_name + "<br>"+ "(" + item.group_name+")套" + "<br></h3></div>"
                            + " <div style =\"border:0px solid #000;width:50%;height:20px;float:left;text-align:center;\"><a href=\"stockunit_show?platform_name=" + escape(item.machine_name) + "\">单元</a></div><div style=\"border:0px solid #000;width:50%;height:20px;float:right;text-align:center;\"><a href=\"/LR_LGManager/stock_info_edit/Index?platform_name=" + escape(item.machine_name) + "\">零件</a></div></div></td>";
                    }
                }
            });
            tbody_content += "</table>"
            $("#lyout").html(tbody_content);
        }
    });
}
    
