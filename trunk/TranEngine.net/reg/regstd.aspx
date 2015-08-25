<%@ Page Language="C#" AutoEventWireup="true" CodeFile="regstd.aspx.cs" Inherits="reg_regstd" Title="培训学员注册" %>
<%@ Import Namespace="TrainEngine.Core" %>

<asp:content id="Content1" contentplaceholderid="cphBody" runat="Server">
<style>

input,select,option{font:12px/14px Arial; vertical-align: middle;}

.pd { margin-left:12px;margin-top:-21px;}

.tbl1{width:100%;}

.tbl1 td{padding:4px 0;}

.tbl1 .td1{text-align:right;}

.tbl1 .td1 span{color:red;}

.tbl1 .td3{color:#666;}

.tbl1 .td3 img{vertical-align:middle;margin-right:3px;}

.tbl1 .head td{padding-left:45px;font:18px/22px "宋体";font-weight:bold;border-top:dashed 1px #336699;padding-top:10px;}



#reg_uid{width:230px}

#reg_pwd1{width:230px}

#reg_pwd2{width:230px}

#reg_email{width:230px}

#reg_nicheng{width:230px}

#reg_mobile{width:230px}

#reg_qqmsn{width:230px}

#reg_company{width:230px}

#reg_phone1{width:30px;}

#reg_phone2{width:92px;}

#reg_phone3{width:60px;}


#txt1{width:500px;height:80px;}




.tr1 td{border-top:dashed 1px  #336699;padding:10px 0;}

.tr1 a{color:#2874BA;}

.tr1 a:hover{color:red;}



.tr2 td{padding-left:200px;}

.td_noscript{font:16px/30px "宋体";font-weight:bold;color:red;}

</style>

<script>
    //阻止默认行为
    function my_stop(oEvent) { if (oEvent.preventDefault) { oEvent.preventDefault() } else { oEvent.returnValue = false; return false; } }
    //
    function fnt_on(target_name) {
        var my_target = document.getElementById(target_name);
        my_target.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/alert.gif' />"
        switch (target_name) {
            case "td_uid": { my_target.innerHTML += "字母,数字 4～20字符"; } break;
            case "td_pwd1": { my_target.innerHTML += "字母,数字 6～20字符"; } break;
            case "td_pwd2": { my_target.innerHTML += "请再次输入密码"; } break;
            case "td_email": { my_target.innerHTML += "正常接收邮件的邮箱"; } break;
            case "td_nicheng": { my_target.innerHTML += "请输入您的姓名"; } break;
            case "td_sex": { my_target.innerHTML += "请选择性别"; } break;
            case "td_diqu": { my_target.innerHTML += "请选择"; } break;
            case "td_company": { my_target.innerHTML += "请正确填写公司名称(有机会获取更多下载积分或申请VIP会员)"; } break;
            case "td_phone": { my_target.innerHTML += "请正确填写,如(020-12345678-888),如无分机请留空"; } break;
            case "td_mobile": { my_target.innerHTML += "请正确填写11位手机号码"; } break;
            case "td_qqmsn": { my_target.innerHTML += "请正确填写以便我们及时联系(例如:qq:123456 msn:abc@abc.com)"; } break;
            case "td_codeimg": { my_target.innerHTML += "请输入验证码"; } break;
            default: { }
        }
    }
    //用户名 ajax=0假 1真
    function fnt_uid(ajax) {
        var reg_uid = document.getElementById("reg_uid");
        var td_uid = document.getElementById("td_uid");
        var rg_uid = /^[\w\d_]{4,20}$/;
        if (reg_uid.value == "") { td_uid.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>必填</font>"; return "请填写用户名"; }
        else {
            if (rg_uid.test(reg_uid.value) == true) { if (ajax == 1) { td_uid.innerHTML = "<font color='red'>......</font>"; ajax_uid(); } else { td_uid.innerHTML = ""; } return ""; }
            else { td_uid.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>错误</font>"; return "用户名错误 字母,数字 4～20字符"; }
        }
    }
    function ajax_uid() {
        var reg_uid = document.getElementById("reg_uid");
        var td_uid = document.getElementById("td_uid");
        var xmlhttp;
        var hdd_validate = document.getElementById("hdd_validate_uid");
        try { xmlhttp = new XMLHttpRequest(); }
        catch (e) { xmlhttp = new ActiveXObject("Microsoft.XMLHTTP"); }
        xmlhttp.open("post", "<%=Utils.AbsoluteWebRoot %>reg/check_user.aspx");
        xmlhttp.setRequestHeader('content-type', 'application/x-www-form-urlencoded');
        xmlhttp.send("user=std&type=uid&txt_value=" + reg_uid.value);
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4) {
                if (xmlhttp.status == 200) {
                    if (xmlhttp.responseText == "none") { td_uid.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/ok.gif' />"; hdd_validate.value = "true"; }
                    else { td_uid.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>已被使用</font>"; hdd_validate.value = "false"; }
                }
                else {
                    td_uid.innerHTML = "<font color='red'>系统出错,请稍候重试,或联系网站管理员获取帮助</font>";
                    alert(xmlhttp.responseText)
                }
            }
        }
    }
    //密码
    function fnt_pwd1() {
        var reg_pwd1 = document.getElementById("reg_pwd1");
        var td_pwd1 = document.getElementById("td_pwd1");
        var rg_pwd1 = /^[\w\d_]{6,20}$/;
        if (reg_pwd1.value == "") { td_pwd1.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>必填</font>"; return "请填写密码"; }
        else {
            if (rg_pwd1.test(reg_pwd1.value) == true) { td_pwd1.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/ok.gif' />"; return ""; }
            else { td_pwd1.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>错误</font>"; return "密码错误 字母,数字 6～20字符"; }
        }
    }
    //确认密码
    function fnt_pwd2() {
        var reg_pwd1 = document.getElementById("reg_pwd1");
        var reg_pwd2 = document.getElementById("reg_pwd2");
        var td_pwd2 = document.getElementById("td_pwd2");
        if (reg_pwd1.value == "") { td_pwd2.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>必填</font>"; return "请填确认码"; }
        else {
            if (reg_pwd1.value != reg_pwd2.value) { td_pwd2.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>确认码与密码不一致</font>"; return "确认码与密码不一致"; }
            else { td_pwd2.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/ok.gif' />"; return ""; }
        }
    }
    //email ajax= 0假 1真
    function fnt_email(ajax) {
        var reg_email = document.getElementById("reg_email");
        var td_email = document.getElementById("td_email");
        var rg_email = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
        if (reg_email.value == "") { td_email.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>必填</font>"; return "请填邮箱"; }
        else {
            if (rg_email.test(reg_email.value) == true) { if (ajax == 1) { td_email.innerHTML = "<font color='red'>......</font>";ajax_email(); } else { td_email.innerHTML = ""; } return ""; }
            else { td_email.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>邮箱格式错误</font>"; return "邮箱格式错误"; }
        }
    }
    function ajax_email() {
        var reg_email = document.getElementById("reg_email");
        var td_email = document.getElementById("td_email");
        var xmlhttp;
        var hdd_validate = document.getElementById("hdd_validate_email");
        try { xmlhttp = new XMLHttpRequest(); }
        catch (e) { xmlhttp = new ActiveXObject("Microsoft.XMLHTTP"); }
        xmlhttp.open("post", "<%=Utils.AbsoluteWebRoot %>reg/check_user.aspx");
        xmlhttp.setRequestHeader('content-type', 'application/x-www-form-urlencoded');
        xmlhttp.send("user=std&type=email&txt_value=" + reg_email.value);
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4) {
                if (xmlhttp.status == 200) {
                    if (xmlhttp.responseText == "none") { td_email.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/ok.gif' />"; hdd_validate.value = "true"; }
                    else { td_email.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>已被使用</font>"; hdd_validate.value = "false"; }
                }
                else {
                    td_uid.innerHTML = "<font color='red'>系统出错,请稍候重试,或联系网站管理员获取帮助</font>";
                    alert(xmlhttp.responseText)
                }
            }
        }
    }
    //联系人
    function fnt_nicheng() {
        var reg_nicheng = document.getElementById("reg_nicheng");
        var td_nicheng = document.getElementById("td_nicheng");
        if (reg_nicheng.value == "") { td_nicheng.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>必填</font>"; return "请填写昵称"; }
        else { td_nicheng.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/ok.gif' />"; return ""; }
    }
    
    //电话
    function fnt_phone() {
        var message = "";
        var reg_phone1 = document.getElementById("reg_phone1");
        var reg_phone2 = document.getElementById("reg_phone2");
        var reg_phone3 = document.getElementById("reg_phone3");
        var td_phone = document.getElementById("td_phone");
        var rg_phone1 = /^\d{3,4}$/;
        var rg_phone2 = /^\d{7,8}$/;
        var rg_phone3 = /^\d{0,6}$/;

        if ((reg_phone1.value != "" | reg_phone2.value != "" | reg_phone3.value != "") & rg_phone1.test(reg_phone1.value) == false) { message += "区号错误,"; }
        if ((reg_phone1.value != "" | reg_phone2.value != "" | reg_phone3.value != "") & rg_phone2.test(reg_phone2.value) == false) { message += "电话号错误,"; }
        if ((reg_phone1.value != "" | reg_phone2.value != "" | reg_phone3.value != "") & rg_phone3.test(reg_phone3.value) == false) { message += "分机号错误,"; }

        if (message == "") { td_phone.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/ok.gif' />"; return ""; }
        else { td_phone.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>" + message + "</font>"; return message; }
    }
    //手机
    function fnt_mobile() {
        var reg_mobile = document.getElementById("reg_mobile");
        var td_mobile = document.getElementById("td_mobile");
        var rg_mobile = /^\d{11}$/
        if (reg_mobile.value == "" | rg_mobile.test(reg_mobile.value) == true) { td_mobile.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/ok.gif' />"; return ""; }
        else { td_mobile.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' />移动电话错误"; return "移动电话错误"; }
    }
    //qqmsn
    function fnt_qqmsn() {
        var td_qqmsn = document.getElementById("td_qqmsn");
        td_qqmsn.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/ok.gif' />";
        return "";
    }
    //公司名称
    function fnt_company() {
        var reg_company = document.getElementById("reg_company");
        var td_company = document.getElementById("td_company");
        td_company.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/ok.gif' />"; return "";
    }
    //省市
    function fnt_sheng_change() {
        var reg_sheng = document.getElementById("reg_sheng");

        var ltl_shi = "请选城市,0|;北京市,1|;天津市,2|;上海市,3|;重庆市,4|;请选城市,0|广州市,5|深圳市,6|珠海市,7|汕头市,8|韶关市,9|河源市,10|梅州市,11|汕尾市,12|东莞市,13|中山市,14|江门市,15|佛山市,16|阳江市,17|湛江市,18|茂名市,19|肇庆市,20|清远市,21|潮州市,22|揭阳市,23|云浮市,24|;请选城市,0|石家庄市,25|唐山市,26|秦皇岛市,27|邯郸市,28|邢台市,29|保定市,30|张家口市,31|承德市,32|廊坊市,33|衡水市,34|沧州市,35|;请选城市,0|郑州市,36|开封市,37|洛阳市,38|平顶山市,39|焦作市,40|鹤壁市,41|新乡市,42|安阳市,43|濮阳市,44|许昌市,45|漯河市,46|三门峡市,47|南阳市,48|商丘市,49|信阳市,50|周口市,51|驻马店市,52|济源市,53|;请选城市,0|济南市,54|青岛市,55|淄博市,56|枣庄市,57|东营市,58|潍坊市,59|烟台市,60|威海市,61|济宁市,62|泰安市,63|日照市,64|莱芜市,65|临沂市,66|德州市,67|聊城市,68|滨州市,69|菏泽市,70|;请选城市,0|太原市,71|大同市,72|阳泉市,73|长治市,74|晋城市,75|朔州市,76|晋中市,77|运城市,78|忻州市,79|临汾市,80|;请选城市,0|哈尔滨市,81|齐齐哈尔市,82|鹤岗市,83|双鸭山市,84|伊春市,85|牡丹江市,86|七台河市,87|黑河市,88|绥化市,89|木斯市,90|鸡西市,91|大庆市,92|;请选城市,0|长春市,93|白城市,94|白山市,95|吉林市,96|辽源市,97|四平市,98|松原市,99|通化市,100|;请选城市,0|沈阳市,101|大连市,102|鞍山市,103|抚顺市,104|本溪市,105|丹东市,106|盘锦市,107|阜新市,108|辽阳市,109|铁岭市,110|朝阳市,111|;请选城市,0|南京市,112|徐州市,113|连云港市,114|淮安市,115|宿迁市,116|盐城市,117|扬州市,118|泰州市,119|南通市,120|镇江市,121|常州市,122|无锡市,123|苏州市,124|;请选城市,0|合肥市,125|芜湖市,126|蚌埠市,127|淮南市,128|马鞍山市,129|淮北市,130|铜陵市,131|安庆市,132|黄山市,133|滁州市,134|阜阳市,135|宿州市,136|巢湖市,137|六安市,138|毫州市,139|池州市,140|宣城市,141|;请选城市,0|杭州市,142|宁波市,143|温州市,144|嘉兴市,145|湖州市,146|绍兴市,147|金华市,148|衢州市,149|舟山市,150|台州市,151|丽水市,152|;请选城市,0|南昌市,153|景德镇市,154|萍乡市,155|九江市,156|新余市,157|鹰潭市,158|赣州市,159|吉安市,160|宜春市,161|抚州市,162|上饶市,163|;请选城市,0|福州市,164|厦门市,165|三明市,166|莆田市,167|泉州市,168|漳州市,169|南平市,170|龙岩市,171|宁德市,172|;请选城市,0|武汉市,173|黄石市,174|襄樊市,175|十堰市,176|荆州市,177|宜昌市,178|荆门市,179|鄂州市,180|孝感市,181|黄冈市,182|咸宁市,183|随州市,184|恩施州,185|仙桃市,186|天门市,187|潜江市,188|;请选城市,0|长沙市,189|株洲市,190|湘潭市,191|衡阳市,192|邵阳市,193|岳阳市,194|常德市,195|张家界市,196|益阳市,197|郴州市,198|永州市,199|怀化市,200|娄底市,201|湘西州,202|;请选城市,0|西安市,203|铜川市,204|宝鸡市,205|咸阳市,206|渭南市,207|延安市,208|汉中市,209|榆林市,210|安康市,211|商洛市,212|;请选城市,0|兰州市,213|金昌市,214|白银市,215|天水市,216|嘉峪关市,217|武威市,218|张掖市,219|平凉市,220|酒泉市,221|庆阳市,222|定西地区,223|陇南地区,224|甘南州,225|宁夏回族自治州,226|;请选城市,0|西宁市,227|海东,228|海北州,229|海南州,230|果洛州,231|玉树州,232|海西州,233|;请选城市,0|成都市,234|自贡市,235|攀枝花市,236|泸州市,237|德阳市,238|绵阳市,239|广元市,240|遂宁市,241|内江市,242|乐山市,243|南充市,244|宜宾市,245|广安市,246|达州市,247|眉山市,248|雅安市,249|巴中市,250|资阳市,251|阿坝州,252|甘孜州,253|凉山州,254|;请选城市,0|昆明市,255|曲靖市,256|玉溪市,257|保山市,258|昭通市,259|思茅,260|临沧,261|丽江,262|文山州,263|红河州,264|西双版纳州,265|楚雄州,266|大理州,267|德宏州,268|怒江州,269|迪庆州,270|;请选城市,0|贵阳市,271|六盘水市,272|遵义市,273|安顺市,274|铜仁,275|毕节,276|黔西南州,277|黔东南州,278|黔南州,279|;请选城市,0|海口市,280|三亚市,281|琼山市,282|文昌市,283|琼海市,284|万宁市,285|五指山市,286|东方市,287|儋州市,288|;请选城市,0|呼和浩特市,289|包头市,290|乌海市,291|赤峰市,292|通辽市,293|鄂尔多斯市,294|呼伦贝尔市,295|乌兰察布盟,296|锡林郭勒盟,297|巴彦淖尔盟,298|阿拉善盟,299|兴安盟,300|;请选城市,0|银川市,301|石嘴山市,302|吴忠市,303|固原市,304|;请选城市,0|乌鲁木齐市,305|克拉玛依市,306|石河子市,307|阿拉尔市,308|图木舒克市,309|五家渠市,310|吐鲁番,311|哈密,312|和田,313|阿克苏,314|喀什,315|克孜勒苏州,316|巴音郭楞州,317|昌吉州,318|博尔塔拉州,319|伊犁州,320|塔城,321|阿勒泰州,322|;请选城市,0|拉萨市,323|那曲,324|昌都,325|山南,326|日喀则,327|阿里,328|林芝,329|;请选城市,0|南宁市,330|柳州市,331|桂林市,332|梧州市,333|北海市,334|防城港市,335|钦州市,336|贵港市,337|玉林市,338|百色市,339|贺州市,340|河池市,341|来宾市,342|崇左市,343|";
        var li_sheng = ltl_shi.split(";");
        var li_shi = li_sheng[reg_sheng.selectedIndex].split("|");

        var reg_shi = document.getElementById("reg_shi");
        reg_shi.length = 0;
        for (i = 0; i <= li_shi.length - 2; i++) {
            var text_value = li_shi[i].split(",");
            reg_shi.options[reg_shi.length] = new Option(text_value[0], text_value[0]);
        }
    }
    function fnt_select(my_value) {
        var reg_shi = document.getElementById("reg_shi");
        for (i = 0; i < reg_shi.length; i++) {
            if (reg_shi.options[i].value == my_value) { reg_shi.options[i].selected = true; }
        }
    }
    function fnt_sheng_shi() {
        var reg_sheng = document.getElementById("reg_sheng");
        var reg_shi = document.getElementById("reg_shi");
        var td_diqu = document.getElementById("td_diqu");

        var message = ""
        if (reg_sheng.options[reg_sheng.selectedIndex].value == 0) { message += "请选择省份,"; }
        if (reg_shi.options[reg_shi.selectedIndex].value == "请选城市") { message += "请选择城市,"; }

        if (message == "") { td_diqu.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/ok.gif' />"; return ""; }
        else { td_diqu.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>" + message + "</font>"; return message; }
    }
    
    //条款
    function fnt_tiaokuan(my_this) {
        var tiaokuan = document.getElementById("tiaokuan");
        var td_tiaokuan = document.getElementById("td_tiaokuan");
        if (tiaokuan.checked) { td_tiaokuan.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/ok.gif' />"; return ""; }
        else { td_tiaokuan.innerHTML = "<img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/error.gif' /><font color='red'>不同意条款不能注册</font>"; return "不同意条款不能注册"; }
    }
    //提交
    function fnt_sbm(oEvent) {
        var hdd_validate = document.getElementById("hdd_validate");
        var hdd_validate_u = document.getElementById("hdd_validate_uid");
        var hdd_validate_e = document.getElementById("hdd_validate_email");
        
        var err_message = "";
        var err_target = "";

        if (err_message == "") { err_message = fnt_uid(0); err_target = "reg_uid"; } else { fnt_uid(0); }
        if (err_message == "") { err_message = fnt_pwd1(); err_target = "reg_pwd1"; } else { fnt_pwd1(); }
        if (err_message == "") { err_message = fnt_pwd2(); err_target = "reg_pwd2"; } else { fnt_pwd2(); }
        if (err_message == "") { err_message = fnt_email(0); err_target = "reg_email"; } else { fnt_email(0); }
        if (err_message == "") { err_message = fnt_nicheng(); err_target = "reg_nicheng"; } else { fnt_nicheng(); }
        if (err_message == "") { err_message = fnt_sheng_shi(); err_target = "reg_sheng"; } else { fnt_sheng_shi(); }
        if (err_message == "") { err_message = fnt_company(); err_target = "reg_company"; } else { fnt_company(); }
        if (err_message == "") { err_message = fnt_phone(); err_target = "reg_phone1"; } else { fnt_phone(); }
        if (err_message == "") { err_message = fnt_mobile(); err_target = "reg_mobile"; } else { fnt_mobile(); }
        if (err_message == "") { err_message = fnt_qqmsn(); err_target = "reg_qqmsn"; } else { fnt_qqmsn(); }
        if (err_message == "") { err_message = fnt_tiaokuan(); err_target = "tiaokuan"; } else { fnt_tiaokuan(); }

        if (hdd_validate_u.value == "false" || hdd_validate_e.value == "false") {
            my_stop(oEvent);
            return false;
        }

        if (err_message == "") { hdd_validate.value = "true"; }
        else {
            my_stop(oEvent);
            //var my_target=document.getElementById(err_target);
            //my_target.focus();
            //alert(err_message);
        }
    }
    //错误显示
    function fnt_err() {
        fnt_uid(1);
        fnt_pwd1();
        fnt_pwd2();
        fnt_email(1);
        fnt_nicheng();
        fnt_sheng_shi();
        fnt_company();
        fnt_phone();
        fnt_mobile();
        fnt_qqmsn();
        fnt_codeimg();
        fnt_tiaokuan();
    }

</script>

<div class="Div_Position" id="DivPosition"  > <span style="padding-left: 12px;font-size:14px;">培训学员注册</span> </div>
<div class="divRegBox">
<div class="div1">

<table class="tbl1" cellpadding="0" cellspacing="0">

<noscript><tr><td class="td_noscript" colspan="3">您的浏览器没有启用javascript功能,暂不能注册,请启用后重新注册</td></tr></noscript>

<tr class="head"><td colspan="3" style="border:none;background:url(<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/btn5.jpg) left bottom no-repeat;">帐号基本信息</td></tr>

<tr><td class="td1" style="width:12%;"><span>*</span> 用户名：</td><td style="width:33%;"><input name="reg_uid" id="reg_uid" type="text" maxlength="20" onblur="fnt_uid(1)" onfocus="fnt_on('td_uid')" /></td><td id="td_uid" class="td3" style="width:55%;">字母,数字 4～20字符</td></tr>

<tr><td class="td1"><span>*</span> 登录密码：</td><td><input name="reg_pwd1" id="reg_pwd1" type="password" maxlength="20" onblur="fnt_pwd1()" onfocus="fnt_on('td_pwd1')" /></td><td id="td_pwd1" class="td3">字母,数字 6～20字符</td></tr>

<tr><td class="td1"><span>*</span> 确认密码：</td><td><input name="reg_pwd2" id="reg_pwd2" type="password" maxlength="20" onblur="fnt_pwd2()" onfocus="fnt_on('td_pwd2')" /></td><td id="td_pwd2" class="td3">请再次输入密码</td></tr>

<tr><td class="td1"><span>*</span> 电子邮箱：</td><td><input name="reg_email" id="reg_email" type="text" maxlength="100" onblur="fnt_email(1)" onfocus="fnt_on('td_email')" /></td><td id="td_email" class="td3">正常接收邮件的邮箱</td></tr>



<tr><td style="height:10px;padding:0;"></td></tr>



<tr class="head"><td colspan="3" style="background:url(<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/btn4.jpg) left bottom no-repeat;">个人详细信息</td></tr>

<tr><td class="td1"><span>*</span> 昵称：</td><td><input name="reg_nicheng" id="reg_nicheng" type="text" maxlength="50" onblur="fnt_nicheng()" onfocus="fnt_on('td_nicheng')" /></td><td id="td_nicheng" class="td3">请输入您的姓名</td></tr>


<tr><td class="td1"><span>*</span> 所在地区：</td><td><select name="reg_sheng" id="reg_sheng" onchange="fnt_sheng_change()" onfocus="fnt_on('td_diqu')" onblur="fnt_sheng_shi()">

	<option value="0">请选省份</option>

	<option value="1">北京市</option>

	<option value="2">天津市</option>

	<option value="3">上海市</option>

	<option value="4">重庆市</option>

	<option value="5">广东省</option>

	<option value="6">河北省</option>

	<option value="7">河南省</option>

	<option value="8">山东省</option>

	<option value="9">山西省</option>

	<option value="10">黑龙江</option>

	<option value="11">吉林省</option>

	<option value="12">辽宁省</option>

	<option value="13">江苏省</option>

	<option value="14">安徽省</option>

	<option value="15">浙江省</option>

	<option value="16">江西省</option>

	<option value="17">福建省</option>

	<option value="18">湖北省</option>

	<option value="19">湖南省</option>

	<option value="20">陕西省</option>

	<option value="21">甘肃省</option>

	<option value="22">青海省</option>

	<option value="23">四川省</option>

	<option value="24">云南省</option>

	<option value="25">贵州省</option>

	<option value="26">海南省</option>

	<option value="27">内蒙古</option>

	<option value="28">宁夏</option>

	<option value="29">新疆</option>

	<option value="30">西藏</option>

	<option value="31">广西省</option>

</select> <select name="reg_shi" id="reg_shi" onfocus="fnt_on('td_diqu')" onblur="fnt_sheng_shi()">

	<option value="0">请选城市</option>

</select></td><td id="td_diqu" class="td3">请选择</td></tr>

<tr><td class="td1">公司全称：</td><td><input name="reg_company" id="reg_company" type="text" maxlength="100" onblur="fnt_company()" onfocus="fnt_on('td_company')" /></td><td id="td_company" class="td3">请正确填写公司名称(有机会获取更多下载积分或申请VIP会员)</td></tr>

<tr><td class="td1">固定电话：</td><td><input name="reg_phone1" id="reg_phone1" type="text" maxlength="4" onblur="fnt_phone()" onfocus="fnt_on('td_phone')" /> - <input name="reg_phone2" id="reg_phone2" type="text" maxlength="8" onblur="fnt_phone()" onfocus="fnt_on('td_phone')" /> - <input name="reg_phone3" id="reg_phone3" type="text" maxlength="6" onblur="fnt_phone()" onfocus="fnt_on('td_phone')" /></td><td id="td_phone" class="td3">请正确填写,如(020-12345678-888),如无分机请留空</td></tr>

<tr><td class="td1">移动电话：</td><td><input name="reg_mobile" id="reg_mobile" type="text" maxlength="11" onblur="fnt_mobile()" onfocus="fnt_on('td_mobile')" /></td><td id="td_mobile" class="td3">请正确填写11位手机号码</td></tr>

<tr><td class="td1">MSN/qq：</td><td><input name="reg_qqmsn" id="reg_qqmsn" type="text" maxlength="100" onblur="fnt_qqmsn()" onfocus="fnt_on('td_qqmsn')" /></td><td id="td_qqmsn" class="td3">请正确填写以便我们及时联系(例如:qq:123456 msn:abc@abc.com)</td></tr>




<tr><td style="height:10px;padding:0;"></td></tr>



<tr class="tr1"><td class="td1">确认用户协议：</td><td><a href="/tiaokuan.shtm" target=_blank>阅读注册服务条款</A> <input name="tiaokuan" id="tiaokuan" type="checkbox" checked="checked" onclick="fnt_tiaokuan(this)" /><label for="tiaokuan">已阅读并接受服务条款</label></td><td id="td_tiaokuan" class="td3">&nbsp;</td></tr>



<tr class="tr2"><td colspan="3"><asp:Button runat="server" Text="确定注册" ID="btnok" onclick="btnok_Click" onclientclick="fnt_sbm(event)"></asp:Button></td></tr>

</table>
<input name="hdd_validate" id="hdd_validate" type="hidden" value="false" />
<input name="hdd_validate" id="hdd_validate_uid" type="hidden" value="false" />
<input name="hdd_validate" id="hdd_validate_email" type="hidden" value="false" />
</div>
</div>
</asp:content>
