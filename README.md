### ValidBox语法
####Fluent Api风格

``@Html.TextBoxFor(item => item.Name, Html.Valid().Required("名称为必填项").Length(2, 4).Remote("/home/CheckName", "Id", "Name").AsHtmlAttribute())``

####打特性风格

public class UserInfo
    {
        [Required]
        public int Id { get; set; }

        [Required(Message = "名称为必填项")]
        [Length(2, 4, Message = "名称为{0}到{1}个字")]
        [Remote("/home/CheckName", "Id", "Name")]
        public string Name { get; set; }

        [Required(Message = "邮箱为必填项")]
        [Email]
        [MaxLength(15)]
        public string Email { get; set; }
    }

####表单提交
``function saveForm() {
   // 前台验证 通过后再提交表单
   $('.block').validInput(function (r) {
      if (r == false) return;
      $.post("/home/save", $('.block').find("input").serialize(), function (data) {
          alert(data);
      });
   });
}``
