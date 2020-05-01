
layui.use(['layer', "form"],
    function () {
        var layer = layui.layer;
        $.ajax({
            type: "get",
            async: true,
            url: "/Create/StudentInfo",
            success: function (data) {
                $(".layui-body").empty();
                $(".layui-body").append(data);
            }
        });
    });

function tea() {
    $.ajax({
        type: "get",
        async: true,
        url: "/Create/TeacherInfo",
        success: function (data) {
            $(".layui-body").empty();
            $(".layui-body").append(data);
        }
    });
}

function stu() {
    $.ajax({
        type: "get",
        async: true,
        url: "/Create/StudentInfo",
        success: function (data) {
            $(".layui-body").empty();
            $(".layui-body").append(data);
        }
    });
}

function room() {
    $.ajax({
        type: "get",
        async: true,
        url: "/Create/ClassInfo",
        success: function (data) {
            $(".layui-body").empty();
            $(".layui-body").append(data);
        }
    });
}

function OpenAddStudent() {
    layer.open({
        type: 2,
        title: '添加学生',
        area: ['400px', '600px'],
        skin: 'layui-layer-rim',
        content: "/Create/CreateStudent"
    });
}
function OpenAddTeacher() {
    layer.open({
        type: 2,
        title: '添加老师',
        area: ['400px', '680px'],
        skin: 'layui-layer-rim',
        content: "/Create/CreateTeacher"
    });
}
function OpenAddClass() {
    layer.open({
        type: 2,
        title: '添加班级',
        area: ['400px', '400px'],
        skin: 'layui-layer-rim',
        content: "/Create/CreateClass"

    });
}