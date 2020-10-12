//reloadpage();
function request(d) { for (var c = location.search.slice(1).split("&"), a = 0; a < c.length; a++) { var b = c[a].split("="); if (b[0] == d) if ("undefined" == unescape(b[1])) break; else return unescape(b[1]) } return "" };
var platform_name = decodeURI(request('platform_name'));
$(document).ready(function () {
    reloadpage();
    setInterval(function () { reloadpage(); }, 500000);
});
function reloadpage() {
    $.ajax({
        url: 'GetStockUnitInfo',
        type: 'GET',
        dataType: 'json',
        data: {
            platform_name: platform_name
        },
        success: function (data) {

            var tbody_content = "<table style=\"border:20px;\"><tr>";
            $.each(data, function (vi, item) {
                if (vi == 0) {
                    tbody_content += "<td>"
                        + "    <div class=\"row row-modal\" id=\"" + item.machine_name + "\" style=\"background-color:rgba(153, 204, 255, 1);height:200px;width:200px;text-align:center;\"> "
                        + "   <div style=\"height:30%;padding-top:50px;\"><h3 class=\"main-font\" style=\"margin-top:0;color:#000;font-weight:800;font-family:SimSun YaHei;\">"
                        + item.machine_name + "<br>" + "(" + item.group_name + ")套" + "</h3></div>"
                        + "<div style=\"border:0px solid #000;height:20px;text-align:center;\"><a href=\"/LR_LGManager/stock_info_edit/Index?platform_name=" + escape(platform_name) + "&unit_name=" + escape(item.machine_name) + "\">零件</a></div></div></td>";
                }
                else {
                    if (vi % 4 == 0) {
                        tbody_content += "</tr><tr><td>"
                            + "    <div class=\"row row-modal\" id=\"" + item.machine_name + "\" style=\"background-color:rgba(153, 204, 255, 1);height:200px;width:200px;text-align:center;\"> "
                            + "   <div style=\"height:30%;padding-top:50px;\"><h3 class=\"main-font\" style=\"margin-top:0;color:#000;font-weight:800;font-family:SimSun YaHei;\">"
                            + item.machine_name + "<br>" + "("+ item.group_name + ")套" + "</h3></div>"
                            + "<div style=\"border:0px solid #000;height:20px;text-align:center;\"><a href=\"/LR_LGManager/stock_info_edit/Index?platform_name=" + escape(platform_name) + "&unit_name=" + escape(item.machine_name) + "\">零件</a></div></div></td>";
                    }
                    else {
                        tbody_content += "<td>"
                            + "    <div class=\"row row-modal\" id=\"" + item.machine_name + "\" style=\"background-color:rgba(153, 204, 255, 1);height:200px;width:200px;text-align:center;\"> "
                            + "   <div style=\"height:30%;padding-top:50px;\"><h3 class=\"main-font\" style=\"margin-top:0;color:#000;font-weight:800;font-family:SimSun YaHei;\">"
                            + item.machine_name + "<br>" + "("+ item.group_name + ")套" + "</h3></div>"
                            + "<div style=\"border:0px solid #000;height:20px;text-align:center;\"><a href=\"/LR_LGManager/stock_info_edit/Index?platform_name=" + escape(platform_name) + "&unit_name=" + escape(item.machine_name) + "\">零件</a></div></div></td>";
                    }
                }
            });
            tbody_content += "</table>"
            $("#lyout").html(tbody_content);
        }
    });
}
    
