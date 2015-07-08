jQuery.validRules = {
    required: {
        message: '该项为必填项'
    },
    email: {
        validator: function (self, param, callBack) {
            callBack(/^\w+(\.\w*)*@\w+\.\w+$/.test(self.val()));
        },
        message: '请输入正确的邮箱'
    },
    url: {
        validator: function (self, param, callBack) {
            var r = /^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(self.val());
            callBack(r);
        },
        message: '请输入正确的网络地址'
    },
    length: {
        validator: function (self, param, callBack) {
            var value = self.val();
            callBack(value.length >= param[0] && value.length <= param[1]);
        },
        message: '长度必须介于{0}到{1}个字'
    },
    equalTo: {
        validator: function (self, param, callBack) {
            var targetValue = $('#' + param[0]).val();
            callBack(self.val() == targetValue);
        },
        message: '两次输入的字符不一至'
    },
    notEqualTo: {
        validator: function (self, param, callBack) {
            var targetValue = $('#' + param[0]).val();
            callBack(self.val() != targetValue);
        },
        message: '输入的内容不能重复'
    },
    match: {
        validator: function (self, param, callBack) {
            var reg = new RegExp(param[0]);
            callBack(reg.test(self.val()));
        },
        message: '请输入正确的值'
    },
    notMatch: {
        validator: function (self, param, callBack) {
            var reg = new RegExp(param[0]);
            callBack(!reg.test(self.val()));
        },
        message: '请输入正确的值'
    },
    minLength: {
        validator: function (self, param, callBack) {
            var value = self.val();
            callBack(value.length >= param[0]);
        },
        message: '长度不能小于{0}个字'
    },
    maxLength: {
        validator: function (self, param, callBack) {
            var value = self.val();
            callBack(value.length <= param[0]);
        },
        message: '长度不能超过{0}个字'
    },
    precision: {
        validator: function (self, param, callBack) {
            var min = param[0];
            var max = param[1];

            var all_0 = "^-?\\d+$";
            var all_gt_0 = "^-?\\d+?\\.\\d{" + Math.max(min, 1) + "," + max + "}$";
            var min_eq_0 = all_0 + "|" + all_gt_0;

            var regex = min + max == 0 ? all_0 : min == 0 ? min_eq_0 : all_gt_0;
            var value = self.val();
            callBack(new RegExp(regex).test(value));
        },
        message: '精度为{0}到{1}位小数'
    },
    range: {
        validator: function (self, param, callBack) {
            var value = self.val() * 1;
            callBack(value >= param[0] && value <= param[1]);
        },
        message: '值要在区间[{0},{1}]内的数'
    },
    rangeInt: {
        validator: function (self, param, callBack) {
            if (/^-?\d+$/.test(self.val())) {
                var value = self.val() * 1;
                callBack(value >= param[0] && value <= param[1]);
            } else {
                callBack(false);
            }
        },
        message: '值要在区间[{0},{1}]内的整数'
    },
    number: {
        validator: function (self, param, callBack) {
            callBack(!isNaN(Number(self.val())));
        },
        message: '请输入正确的数值'
    },
    remote: {
        validator: function (self, param, callBack) {
            if (this.ajax && this.ajax.readyState != 4) {
                this.ajax.abort();
            }

            var array = [];
            $.each(param, function (k, v) {
                if (k > 0) array.push(v + '=' + $('#' + v).val());
            });

            this.ajax = $.post(param[0], array.join('&'), function (data) {
                if (data && typeof data === "object") {
                    if (data.message) {
                        $.validRules.remote.message = data.message;
                    }
                    callBack(!!data.state);
                } else {
                    callBack(false);
                }
            }, 'json');
        },
        ajax: undefined,
        message: '请输入正确的值'
    }
};


(function ($) {
    // 隐藏提示信息
    function hideError(self, hideImage) {
        $('.validbox-tip').remove();
        if (hideImage) self.removeClass("valid-error");
        return self;
    };

    // 显示错误信息
    function showError(self, message) {
        hideError(self, true).addClass("valid-error");

        var left = self.offset().left + self.outerWidth();
        var top = self.offset().top + self.outerHeight() / 2 - 10;
        var html = '<div class="validbox-tip"><span class="m">' + message + '</span><span class="p"></span></div>';
        $(html).css('left', left).css("top", top).appendTo("body");

        var offsetTop = top - $(document).scrollTop();
        if (offsetTop <= 0 || offsetTop >= $(window).height()) {
            $("body").animate({ scrollTop: top - $(window).height() / 2 - self.height() / 2 }, 400);
        }
        // 把信息缓存，在mouseEnter时使用
        self.data('message', message);
        return self;
    };

    // 合成信息
    function getMessage(message, param) {
        for (var i = 0; i < param.length; i++) {
            var reg = new RegExp('\\{' + i + '\\}', 'gm');
            message = message.replace(reg, param[i]);
        }
        return message;
    }

    // 获取是否为必须输入 
    function getRequired(self) {
        var key = 'required';
        var rule = self.data(key);

        if (!rule) {
            rule = { required: !!self.attr(key), message: self.attr("required-message") }
            self.data(key, rule);
        }
        return rule;
    };

    // 获取相关的验证规则
    function getRules(self) {
        var key = 'rule';
        var rules = self.data(key);
        if (rules) {
            return rules;
        }

        var rules = eval(self.attr("valid-rule")) || [];
        for (var i = 0; i < rules.length; i++) {
            rules[i].p = rules[i].p || [];
        }
        self.data(key, rules);
        return rules;
    };

    // 对规则进行验证
    function validRules(self, func) {
        var required = getRequired(self);

        // 不必输入且没有输入
        if (required.required == false && ($.trim(self.val() || '')).length == 0) {
            hideError(self, true);
            if ($.isFunction(func)) func(true);
            return;
        }
        // 必须输入而未输入
        if (required.required && ($.trim(self.val() || '')).length == 0) {
            showError(self, required.message || $.validRules.required.message);
            if ($.isFunction(func)) func(false);
            return;
        }

        var index = 0;
        var rules = getRules(self);

        var _validRules = function () {
            if (index >= rules.length) {
                hideError(self, true);
                if ($.isFunction(func)) func(true);
            } else {
                var rule = rules[index++];
                var validRule = $.validRules[rule.n];
                validRule.validator(self, rule.p, function (r) {
                    if (r == false) {
                        var message = getMessage(rule.m || validRule.message, rule.p);
                        showError(self, message);
                        if ($.isFunction(func)) func(r);
                    } else {
                        _validRules();
                    }
                });
            }
        };
        _validRules();
    };

    // 初始化
    function _init() {
        // 绑定事件验证
        var seletor = ".validBox";
        $(document).on("keyup focus", seletor, function () {
            validRules($(this));
        }).on("mouseenter", seletor, function () {
            var self = $(this);
            if (self.hasClass("valid-error")) showError(self, self.data('message') || self.attr("message"));
        }).on("mouseleave blur", seletor, function () {
            var self = $(this);
            if (self.hasClass("valid-error")) hideError(self, false);
        }).on("submit", "form", function (e) {
            $(this).validBox(function (r) { if (!r) e.preventDefault(); });
        }).on("click", "[type=submit]", function (e) {
            e.preventDefault();
            $(this).parents("form").submit();
        });
    };

    // 验证元素的输入
    $.fn.validBox = function (func) {
        var context = this;
        var self = $(this);
        var boxs = self.filter(function () { return $(this).hasClass("validBox") });
        if (boxs.length == 0) boxs = self.find(".validBox");

        var index = 0;
        var _validBox = function () {
            if (index >= boxs.length) {
                if ($.isFunction(func)) func.call(context, true);
            } else {
                var box = $(boxs[index++]);
                validRules(box, function (r) {
                    if (r == false) {
                        if ($.isFunction(func)) func.call(context, r);
                    } else {
                        _validBox();
                    }
                });
            }
        };

        _validBox();
        return self;
    };

    _init();

})(jQuery);
