
var $;

layui.use(['element', 'layer', 'form', "table"],
    function() {
        var element = layui.element;
        var layer = layui.layer;
        var form = layui.form;
        var table = layui.table;
        $ = layui.jquery;
        form.on("select(studentGrade)",
            function(grade) {
                $.ajax({
                    type: "get",
                    async: true,
                    url: "/DataReadOrWrite/AjaxActionResult",
                    data: "id=" + grade.value,
                    dataType: "json",
                    success: function(source) {
                        var i = 0;
                        $("#ClassName").empty();
                        for (i; i < source.length; i++) {
                            $("#ClassName")
                                .append("<option value=\"" + source[i] + "\">" + source[i] + "</option>");
                        }
                        form.render();
                    }
                });
            });
        table.on('edit(test3)', function (obj) {
            $.ajax({
                type: "post",
                //header: { "__RequestVerificationToken": "@Html.AntiForgeryToken()" },
                url: "/DataReadOrWrite/UpdateScore",
                async: true,
                data: {
                    studentInfoId: obj.data.StudentInfoId,
                    classInfoId: obj.data.ClassInfoId,
                    newScore: obj.value,
                    course: obj.field
                },
                success: function (data) {
                    if (data === "error") {
                        layer.msg("请求失败");
                    }

                }
            });
        });
        $(document).ready(function () {


            form.on("submit(class)",
                function (data) {
                    var urls = "";
                    //var token = $('@Html.AntiForgeryToken()').value;
                    var headers = {};
                    //headers["__RequestVerificationToken"] = token;
                    console.log(JSON.stringify(data.field));
                    var datas = data.field;
                    urls = "/DataReadOrWrite/CreateClass";
                    AjaxAddForm(urls, datas);
                });

            form.on("submit(student)",
                function (data) {
                    var urls = "";
                    //var token = $('@Html.AntiForgeryToken()').value;
                    var headers = {};
                    //headers["__RequestVerificationToken"] = token;
                    console.log(JSON.stringify(data.field));
                    var datas = data.field;
                    urls = "/DataReadOrWrite/CreateStudent";
                    AjaxAddForm(urls, datas);
                });
            form.on("submit(teacher)",
                function (data) {
                    var urls = "";
                    //var token = $('@Html.AntiForgeryToken()').value;
                    var headers = {};
                    //headers["__RequestVerificationToken"] = token;
                    console.log(JSON.stringify(data.field));
                    var datas = data.field;
                    urls = "/DataReadOrWrite/CreateTeacher";
                    AjaxAddForm(urls, datas);
                });
        });
    });

        









function AjaxAddForm(urls,  datas) {
    $.ajax({
        type: "post",
        async: true,
        url: urls,
        //headers: headers,
        data: datas,
        success: function (source) {
            if (source != -1 || source != 0) {
                layer.msg("操作成功", { icon: 6 });
            }
            if (source === -1) {
                layer.msg("操作失败", { icon: 5 });
            }
        },
        error: function (source) {
                layer.msg("操作失败", { icon: 5 });
            
        }
    });
}