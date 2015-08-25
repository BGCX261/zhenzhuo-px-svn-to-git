(function(){var C=/^\w+:\/\/([^\/]+)/;var D=/\.ctrip\.com|\.sh\.ctriptravel\.com/;var q=/^([\w-]+\.)?(.+)\.(com|net|org|info|biz|cc|tv|cn|gov\.cn|name|mobi)$/;var E=/ctrip\.com\.hk|ctrip\.com\.cn|sh\.ctriptravel\.com/;var F=/english\.|\.english\./;var r="Session";var G="720";var s="SmartLinkCode={si}&SmartLinkKeyWord={ke}&SmartLinkQuary={qu}&SmartLinkHost={ho}&SmartLinkLanguage={la}";var t=['google|google|q=|UTF-8','baidu|baidu|wd=;word=;kw=|URLEncode','yahoo|yahoo|p=|UTF-8','yahoo|yisou|search:|UTF-8','yahoo|3721|p=|URLEncode','sohu|sogou.com|query=|URLEncode','sohuwz|sohu.com|query=|URLEncode','sinawz|sina.com||','sina|iask.com|k=;_1=;key=|URLEncode','163wz|126.com|q=|URLEncode','163wz|163.com|q=|URLEncode','163wz|188.com|q=|URLEncode','163wz|yeah.net|q=|URLEncode','163|163.com|q=|URLEncode','tom|tom.com|word=;w=|URLEncode','hao123|hao123.com||','265|265.com||','21cn|21cn.com|keyword=|URLEncode','qqwz|qq.com||','qq|soso.com|w=|URLEncode','online|online.sh||','9991|9991.com||','live|msn|q=|UTF-8','yodao|yodao|q=|UTF-8','lycos|lycos|query=|UTF-8','ask|.ask.com|q=|UTF-8','altavista|altavista|q=|UTF-8','search|search.com|q=|UTF-8','netscape|netscape|query=|UTF-8','zhongsou|zhongsou|w=;word=|URLEncode','alice|alice.it|qs=|','teoma|teoma|q=|UTF-8','earthlink|earthlink|q=|UTF-8','cnn|cnn|query=|','looksmart|looksmart|key=|UTF-8','about|about|terms=|','excite|excite|qkw=;q_all=|','mamma|mamma|query=|UTF-8','alltheweb|alltheweb|q=|UTF-8','gigablast|gigablast|q=|UTF-8','aol|aol|query=|UTF-8'];var k=document.referrer;var h=k.match(C);if(h&&!D.test(h=h[1])){var u=document.domain;var v=F.test(u)?'en':'zh';var H=location.href;var i={};var w=[];var l;if((i[r]=I())||(i[r]=J())){var x=(location.search||"").match(new RegExp("[\\?&]isctrip=([^&]+)","i"));if((x?unescape(x[1]):null)=='T'){return}i.expires=new Date((new Date()).getTime()+G*3600000).toGMTString();i.path='/';i.domain=u.match(E)||'ctrip.com';for(var y in i)w.push(y+'='+i[y]);document.cookie=w.join('; ');var z=["src%5Fname","src%5Fkeyword","src%5FQuary","src%5Fhost"];for(var m=0,K=z.length;m<K;m++){document.cookie=z[m]+"=; domain="+i.domain+"; path=/; expires="+new Date(0).toGMTString()}}}function I(){if(l=H.match(new RegExp('[/?&]c=([^&]+)')))return 0;var b='',f='',a='',g='';var d,c,n=0,L=t.length;while(n<L){c=t[n].split('|');if(h.indexOf(c[1])>-1){b=c[0];g=c[3];for(var o=0,A=c[2].split(';'),B,M=A.length;o<M;o++)if(B=k.match(new RegExp('[/?&]'+A[o]+'([^&]+)'))){a=B[1];break}break}n++}if(!b){b=q.test(h)?h.match(q)[2]:'';if(!b)return 0}else if(b=='google'&&/gb2312/i.test(k)||g=='URLEncode')a='_2.'+a;else if(g=='UTF-8')a='_0.'+decodeURIComponent(a);else a='_0.'+a;a=escape(a);d={si:b,ke:f,qu:a,ho:h,la:v};return s.replace(/\{(\w+)\}/g,function(p,j){return d[j]})}function J(){if(!l)return 0;var b=l[1],f='',a='',g='',d,c;if(b.indexOf('A')>-1){c=b.split('A');f=c[0];a=c[1]}if(!f)return 0;else a=(a==1)?'':a;a=escape(a);d={si:f,ke:a,qu:g,ho:h,la:v};return s.replace(/\{(\w+)\}/g,function(p,j){return d[j]})}})();(function(){try{var b=location.search||""}catch(e){var b=document.URL.match(/\?[^#]+/)||""}function getQuery(p){var j=b.match(new RegExp("[\\?&]"+p+"=([^&]+)","i"));return j?unescape(j[1]):null};var f=[],a;var g=["campaign","adid"];for(var d=0;d<g.length;d++){a=getQuery(g[d]);if(a)f.push(g[d]+"="+escape(a))}if(f.length){var c=document.domain.match(/ctrip\.com\.hk|ctrip\.com\.cn|sh\.ctriptravel\.com/)||"ctrip.com";document.cookie="traceExt="+f.join("&")+"; domain="+c+"; path=/; expires="+new Date((new Date()).getTime()+31*24*3600000).toGMTString()}})();

if (!$$.module.searchBox)
	$$.module.searchBox={};
if (!$$.module.pkgSearch)
	$$.module.pkgSearch={};
	
$$.module.searchBox.airHotelList="BJS,SHA,CKG,DLC,TAO,NKG,HGH,XMN,CTU,SZX,CAN,KWL,KMG,LJG,CSX,SIA,WUH,TSN,HRB,KWE,URC,HAK,HET,TYN,FOC,HFE,NGB,KHN,NNG,SHE,WNZ,CGO";

$$.module.pkgSearch.pkgStartCityHash=eval($s2t('({"珠海":31,"拉萨":41,"海口":42,"银川":99,"喀什":109,"西宁":124,"运城":140,"包头":141,"海拉尔":142,"常州":213,"绵阳":370,"泉州":406,"石家庄":428,"汕头":447,"威海":479,"西昌":494,"徐州":512,"延吉":523,"榆林":527,"烟台":533,"义乌":536,"台州":578,"北京":1,"上海":2,"广州":32,"深圳":30,"杭州":17,"成都":28,"南京":12,"青岛":7,"厦门":25,"武汉":477,"沈阳":451,"济南":144,"宁波":375,"无锡":13,"温州":491,"天津":3,"重庆":4,"西安":10,"郑州":559,"福州":258,"昆明":34,"长沙":206,"大连":6,"贵阳":38,"乌鲁木齐":39,"兰州":100,"呼和浩特":103,"太原":105,"长春":158,"合肥":278,"南昌":376,"南宁":380,"哈尔滨":5,1:"北京",2:"上海",32:"广州",30:"深圳",17:"杭州",28:"成都",12:"南京",7:"青岛",25:"厦门",477:"武汉",451:"沈阳",144:"济南",375:"宁波",13:"无锡",491:"温州",3:"天津",4:"重庆",10:"西安",559:"郑州",258:"福州",34:"昆明",206:"长沙",6:"大连",38:"贵阳",39:"乌鲁木齐",100:"兰州",103:"呼和浩特",105:"太原",158:"长春",    278:"合肥",376:"南昌",380:"南宁",5:"哈尔滨",31:"珠海",41:"拉萨",42:"海口",99:"银川",109:"喀什",124:"西宁",140:"运城",141:"包头",142:"海拉尔",213:"常州",370:"绵阳",406:"泉州",428:"石家庄",447:"汕头",479:"威海",494:"西昌",512:"徐州",523:"延吉",527:"榆林",533:"烟台",536:"义乌",578:"台州"})'));
	
$$.module.searchBox.init=function(){this.funcList.each();};
$$.module.searchBox.funcList=[function(){
		$$.module.address.source.fltInternational_cn=$s2t($$.module.address.source.fltInternational_cn);
		$$.module.address.source.fltInternational=$s2t($$.module.address.source.fltInternational);
		$$.module.address.source.fltInternationalStart=$s2t($$.module.address.source.fltInternationalStart);
		$$.module.address.source.fltInternationalTicket=$s2t($$.module.address.source.fltInternationalTicket);
		$$.module.address.source.fltAll=$s2t($$.module.address.source.fltAll);
		$$.module.address.source.fltDomestic=$s2t($$.module.address.source.fltDomestic);
		$$.module.address.source.fltDomesticTicket=$s2t($$.module.address.source.fltDomesticTicket);
		$$.module.address.source.hotel=$s2t($$.module.address.source.hotel);
		$$.module.address.source.internationHotel = $s2t($$.module.address.source.internationHotel);
		//$parserRe($("searchBox"));
	}];


/*index*/
//酒店
$$.module.searchBox.funcList.push(function(){
	var form=__.forms["hotelForm"];
	var hotelSel=form["hotelSel"];
	var perdate=form["perdate"].value,postdate=form["postdate"].value;
	var perdateCalc=perdate.isDateTime(),postdateCalc=postdate.isDateTime();
	var cityname=form["cityname"],citynameInter=form["citynameInter"],cityId=form["cityId"],city=form["city"],districtId=form["DistrictId"];
	var starttime=form["starttime"],deptime=form["deptime"];
	//var Price=form["Price"],PriceInter=form["PriceInter"],BegPrice=form["BegPrice"],EndPrice=form["EndPrice"];
	var hotelSel=form["hotelSel"],country=form["country"],oricity=form["oricity"],roomNum=form['rooms'].parentNode;
	var hotelSwitch=$("hotelSwitch");
	var hotelTag=true;

	//初始化 国内酒店还是海外酒店
	function hotelTypeInit(){
		hotelTag=hotelSel[0].checked;
		roomNum.style.display=citynameInter.style.display=hotelTag?"none":"";
		cityname.style.display=hotelTag?"":"none";
	}

	hotelTypeInit();
	
	for (var i=0,n=hotelSel.length;i<n;i++)
		$(hotelSel[i]).$r("click",hotelTypeInit);

	form.onsubmit=function(){
		var flag=[];
		if (cityname.isNull()&&hotelTag){
			$alert(cityname, $s2t("请输入宾馆所在城市"));
			return false;
		}
		if (citynameInter.isNull()&&!hotelTag){
			$alert(citynameInter,$s2t("请输入宾馆所在城市"));
			return false;
		}
		if (starttime.isNull()){
			$alert(starttime,$s2t("请输入入住时间"));
			return false;
		}
		flag[0]=starttime.value.isDateTime();
		if (!flag[0]){
			$alert(starttime,$s2t("入住时间不符合格式规范或无效的日期"));
			return false;
		}
		if (perdateCalc&&flag[0]<perdateCalc){
			$alert(starttime,$s2t("入住时间不能早于")+perdate);
			return false;
		}
		if (deptime.isNull()){
			$alert(deptime,$s2t("请输入离店时间"));
			return false;
		}
		flag[1]=deptime.value.isDateTime();
		if (!flag[1]){
			$alert(deptime,$s2t("离店时间不符合格式规范或无效的日期"));
			return false;
		}
		if (flag[1]<=flag[0]){
			$alert(deptime,$s2t("离店时间不能早于或等于入住时间")+starttime.value);
			return false;
		}
		if (flag[1]-flag[0]>2419200000){
			$alert(deptime,$s2t("入住时间段不能超过28天"));
			return false;
		}
		//var priceRange=(hotelTag?Price:PriceInter).value.split("|");
		//BegPrice.value=priceRange[0];
		//EndPrice.value=priceRange[1];

//		配合国内酒店新数据使用   //cdchu
//		function adjustCityId(id){
//			id=parseInt(id,10);
//			if (id<20000)
//				return id-100;
//			if (id<80000)
//				return id-20000;
//			return id-80000;
//		}

		function setCityId(id){
			if (id > 0){
				if (id < 20000)
					return (id - 100);
				else
					return (id - 20000);
			}
		}
		
		function isTWCity(){ //判断如果是台湾的城市，跳转到海外酒店
			var cv = cityname.value;
			var TW = ["台北", "高雄", "垦丁", "台北县", "桃园县"];
			for(var i=0,l=TW.length; i<l; i++){
				if(cv == $s2t(TW[i])){return true;}
			}
			return false;
		}
		
		if(hotelTag){  //国内酒店
			if(isTWCity()){// 判断如果是台湾的城市，跳转到海外酒店
				//city.value = setCityId(cityId.value);
				city.value = cityId.value;
				form.action = form.getAttribute("twhotelAction");
				cityId.value="";
			}else{ //非台湾城市，国内ID的处理逻辑
				if (/^D/.test(cityId.value)){
					districtId.value=cityId.value;
					city.value="";
					cityId.value="";
				}else{
					city.value=cityId.value;
					districtId.value="";
				}
			}
		}else{   //海外酒店
			city.value = setCityId(oricity.value);
			cityId.value="";
			form.action = form.getAttribute("interhotelAction");
		}
		return true;
	};
});
//机票
function getRadioValue(obj){
	for (var i=0;i<obj.length;i++)
		if (obj[i].checked)
			return obj[i].value;
	return null;
}
function setRadioValue(obj,value){
	for (var i=0;i<obj.length;i++)
		if (obj[i].value==value)
			return obj[i].checked=true;
	return null;
}
$$.module.searchBox.funcList.push(function(){
	var form=__.forms["flightForm"];
	var flightway=form["flightway"];
	var flightBackFlag=$("flightBackFlag");
	var homecity=form["homecity_name"],destcity1=form["destcity1_name"];
	var HomeCityID=form["HomeCityID"],destcityID=form["destcityID"];
	var DDatePeriod1=form["DDatePeriod1"],ADatePeriod1=form["ADatePeriod1"],today=form["today"];
	var destcity1Code=$("destcity1");
	var flightTag = true; //是否是国内机票
	var flightSwitch = $("flightSwitch");
	
	function changeFlightType(){	
		flightBackFlag.style.visibility=getRadioValue(flightway)=="Single"?"hidden":"";
	};
	
	flightway[0].onclick=flightway[1].onclick=changeFlightType;
	changeFlightType();
	
	//切换国内机票和国际机票的数据源
	if(flightSwitch){
		//初始化国内机票 or 国际机票
		flightTag = $("flightTag").value == "true";
		setFlightSource();
	
		flightSwitch.$r("click", function(e){
			var el = $fixE(e).$target;
			if(el.tagName == "A"){
				flightTag = el.getAttribute("tag") == "fltDomestic";
				if($("flightTag").value == flightTag.toString()){return;}
				
				$("flightTag").value = flightTag; //回退保存时使用
				
				//切换时更替source
				setFlightSource();
				
				//清空原本的数据
				resetFlight();
			}
		});
	}
	
	function setFlightSource(){
		if(flightTag){ //国内机票
			flightSwitch.$("a")[0].className = "current";
			flightSwitch.$("a")[1].className = "";
			flightSwitch.className = "flts_channel_dom";
			$(homecity).module.address.source = "fltDomestic";
			$(destcity1).module.address.source = "fltDomestic";
			form.action = flightSwitch.$("a")[0].getAttribute("faction");
		}else{   //国际机票
			flightSwitch.$("a")[0].className = "";
			flightSwitch.$("a")[1].className = "current";
			flightSwitch.className ="flts_channel_int";
			$(homecity).module.address.source = "fltInternationalStart" ;
			$(destcity1).module.address.source = "fltInternational";
			form.action = flightSwitch.$("a")[1].getAttribute("faction");
		}
		
	}
	
	function resetFlight(){
		homecity.value = destcity1.value = DDatePeriod1.value = ADatePeriod1.value = "";
		$(homecity).module.notice.check();
		$(destcity1).module.notice.check();
		$(DDatePeriod1).module.notice.check();
		$(ADatePeriod1).module.notice.check();
		setRadioValue(flightway, "Single");
		flightBackFlag.style.visibility = "hidden";
	}
	
	function isIntFlt(){return !flightTag;}
	
	form.onsubmit=function(){
		if (homecity.isNull()){
			$alert(homecity,$s2t("请选择您的出发地"));
			return false;
		}
		if (homecity.module.address)
			homecity.module.address.check();
		if (destcity1.isNull()){
			$alert(destcity1,$s2t("请选择您的目的地"));
			return false;
		}
		if (homecity.value==destcity1.value){
			$alert(destcity1,$s2t("您选择的出发地点与目的地相同,请重新选择"));
			return false;
		}
		if (DDatePeriod1.isNull()){
			$alert(DDatePeriod1,$s2t("请选择您的出发日期"));
			return false;
		}
		var d1=DDatePeriod1.value.isDateTime();
		if (!d1){
			$alert(DDatePeriod1,$s2t("出发日期不符合格式规范或无效的日期"));
			return false;
		}
		var d3=today.value.isDateTime();
		if (d3>d1){
			$alert(DDatePeriod1,$s2t("出发日期不能早于")+today.value);
			return false;
		}
		//首页机票日期输入限定优化 一年内可选
		var d4 =new Date(d3.getFullYear()+1,d3.getMonth(),d3.getDate());
		if(d4<d1){
			$alert(DDatePeriod1,$s2t("只能查询一年内航班"));
			return false;
		}
		if (ADatePeriod1.isNull()&&getRadioValue(flightway)=="Double"){
			setRadioValue(flightway,"Single");
			changeFlightType();
		}
		if (getRadioValue(flightway)=="Double"){
			var d2=ADatePeriod1.value.isDateTime();
			if (!d2){
				$alert(ADatePeriod1,$s2t("返回日期不符合格式规范或无效的日期"));
				return false;
			}
			if (d2<d1){
				$alert(ADatePeriod1,$s2t("返回日期不能早于出发日期")+DDatePeriod1.value);
				return false;
			}
			//首页机票日期输入限定优化 一年内可选
			if(d4<d2){
				$alert(ADatePeriod1,$s2t("只能查询一年内航班"));
				return false;
			}
		}

		//国际机票判断
		var PType=form["PType"];
		var flightclass=form["flightclass"];
		if (isIntFlt()){
			//将上海(虹桥)，上海(浦东)，北京(首都)，北京(南苑)过滤成上海|北京
			var specialCity = ["上海(虹桥)","上海(浦东)","北京(首都)","北京(南苑)"];
			var hc = homecity.value;
			for(var i=0,l=specialCity.length;i<l;i++){
				if(hc==$s2t(specialCity[i])){
					homecity.value = hc.replace(/\(.+\)/,"");
					break;
				}
			}
			if (ADatePeriod1.isNull())
				ADatePeriod1.value="";
			flightclass.value="I";
			//出发城市
			if (!fillCode("fltInternationalStart",homecity,HomeCityID)){
				$alert(homecity,$s2t("你选择的出发城市没有前往")+destcity1.value+$s2t("的航班，请重新选择"));
				return false;
			}
			//目的城市
			if (!fillCode("fltInternational",destcity1,destcityID)){
				$alert(destcity1,$s2t("你选择的出发城市没有前往该目的城市的航班，请重新选择"));
				return false;
			}
			form["ticketagency_list"].value=form["homecity_name"].value;
			form["ticketagencyID"].value=form["homecity"].value;
			//form.action="http://flights.ctrip.com/International/ShowFareFirst.aspx";
			//form.action="http://flights."+getDomain()+"/International/ShowFareFirst.aspx";
		}
		return true;
	};
	function getDomain(){
		var arr=location.hostname.match(/(big5\.)?ctrip\.com|(big5\.)?([^\.]+).sh.ctriptravel.com$/);
		return arr&&!/^local$/i.test(arr[2])?arr[0]:"ctrip.com";
	}
	function fillCode(sourceName,fromObj,toObj){
		var source=$$.module.address.source[sourceName];
		if (!source)
			return false;
		var re=new RegExp("@[^\\|]*\\|"+fromObj.value.replace(/([\.\\\/\+\*\?\[\]\{\}\(\)\^\$\|])/g,"\\$1")+"[^@]*","i");
		var arr=source.match(re);
		if (!arr)
			return false;
		toObj.value=arr[0].match(/^@[^\|]*\|[^\|]*\|([^\|@]*)/)[1];
		return true;
	}
});
//机票+酒店
$$.module.searchBox.funcList.push(function(){
	var airHotel=$("airHotel");
	var airHotelBtn=$("airHotelBtn");
	var airHotelFrame=$("airHotelFrame");
	var initFlag=false;
	if (!airHotel|!airHotelBtn)
		return;
	airHotelBtn.$r("click",function(){
		airHotel.style.display=airHotel.style.display?"":"none";
		if (airHotelFrame&&!initFlag){
			airHotelFrame.src=airHotelFrame.getAttribute("tagSrc");
			initFlag=true;
		}
	});
	var searchBoxUl=$("searchBoxUl");
	var airHotelClose=$("airHotelClose");
	if (airHotelClose){
		airHotelClose.$r("click",function(){
			airHotel.style.display="none";
		});
		setInterval(function(){
			if (searchBoxUl&&searchBoxUl.module.tab&&searchBoxUl.module.tab.index!=1)
				airHotel.style.display="none";
		},100);
	}
	___.$r("mousedown",function(e){
		e=$fixE(e);
		var obj=e.$target;
		if (obj==airHotelBtn)
			return;
		while (obj!=airHotel&&obj&&obj!=__.body)
			obj=obj.$parentNode();
		if (obj!=airHotel)
			airHotel.style.display="none";
	});
});
//度假
$$.module.searchBox.funcList.push(function(){
	var pkgSearch=$("pkgSearch");
	var inputList=pkgSearch.$("input");
	var selectList=pkgSearch.$("select");
	var pkgEle={
		form:__.forms["packageForm"],
		startCity:inputList[0],
		startCityCode:inputList[3],
		startCityDiv:$("pkgStartCityDiv"),
		destCity:inputList[1],
		destCityType:inputList[4],
		destCityCode:inputList[5],
		destCityId:inputList[6],
		destCityDiv:$("pkgDestCityDiv"),
		submitBtn:inputList[2],
		startCityPY:inputList[7]
	};
	var pkgDestFlag={destCityCode:null,destCityType:null,destCityId:null};
	//初始化destCity
	pkgEle.destCity.init=function(){
		if (pkgEle.startCitySpan)
			pkgEle.startCitySpan.innerHTML=pkgEle.startCity.value;
		if (!pkgEle.destCity.module.address){
			setTimeout(arguments.callee,500);
			return;
		}
		var str=pkgEle.startCity.value.trim();
		pkgEle.destCity.module.address.source=str?"pkg_"+str:"pkgAll";
	};
	pkgEle.destCity.init();
	//注册浮出Div事件
	pkgEle.startCity.$r("focus",function(){
		pkgEle.startCity.blurFlag=false;
		pkgEle.startCityDiv.style.display="";
		pkgEle.startCityDiv.$setIframe();
	});
	pkgEle.startCity.$r("blur",function(){
		if (pkgEle.startCity.blurFlag){
			setTimeout(function(){
				pkgEle.startCity.focus();
			},0);
			return;
		}
		pkgEle.startCityDiv.style.display="none";
		pkgEle.startCityDiv.$clearIframe();
	});
	pkgEle.destCity.$r("mousedown",function(){
		$$.module.address.source.pkgAll_hotData=null;
	});
	pkgEle.destCity.$r("focus",function(){
		if (!pkgEle.destCity.module.notice||!pkgEle.destCity.module.address){
			pkgEle.destCity.blur();
			return;
		}
		setTimeout(function(){
			pkgEle.destCity.module.notice.enabled=false;
		},0);
		pkgEle.destCity.blurFlag=false;
		pkgEle.destCity.clock=setInterval(function(){
			var flag=pkgEle.destCity.value.trim();
			pkgEle.destCityDiv.style.display=flag?"none":"";
			if (flag)
				pkgEle.destCityDiv.$clearIframe();
			else
				pkgEle.destCityDiv.$setIframe();
		},100);
	});
	pkgEle.destCity.$r("blur",function(){
		if (!pkgEle.destCity.module.notice||!pkgEle.destCity.module.address){
			pkgEle.destCity.blur();
			return;
		}
		clearInterval(pkgEle.destCity.clock);
		if (pkgEle.destCity.blurFlag){
			setTimeout(function(){
				if ($$.browser.IE)
					pkgEle.destCity.focus();
				else
					pkgEle.destCity.$click();
			},0);
			return;
		}
		pkgEle.destCity.module.notice.enabled=true;
		pkgEle.destCityDiv.style.display="none";
		pkgEle.destCityDiv.$clearIframe();
	});
	//初始化destCityDiv
	if (pkgEle.destCityDiv&&$$.module.pkgQuickSearch){
		var dlList=pkgEle.destCityDiv.$("dl");
		var classList=["inChina","outChina","theme"];
		for (var i=0;i<Math.min(dlList.length,3);i++){
			var suggestList=$$.module.pkgQuickSearch[classList[i]];
			if (suggestList){
				for (var j=0;j<suggestList.length;j++){
					var dd=dlList[i].appendChild($c("dd"));
					if(/@/.test(suggestList[j])){
						dd.innerHTML = suggestList[j].replace(/([^@]*)@([^@]*)@([^@]*)/g , function(s , s1 ,s2 ,s3){
							return "<a href=\"javascript:void(0);\" dt=\""+s2+"\" dc=\""+s3+"\"  title=\""+s1+"\">"+s1+"</a>";
						});
					}else{
						dd.innerHTML="<a href=\"javascript:void(0);\" title=\""+suggestList[j]+"\">"+suggestList[j]+"</a>";
					}
					//dd.innerHTML="<a href=\"javascript:void(0);\" title=\""+suggestList[j]+"\">"+suggestList[j]+"</a>";
				}
			}
		}
	}
	//注册点击事件
	pkgEle.startCityDiv.$r("mousedown",function(e){
		var obj=$fixE(e).$target;
		if (obj.tagName=="A"){
			var txt=obj.innerHTML.trim();
			pkgEle.startCity.value=obj.innerHTML.trim();
			pkgEle.startCityPY.value = obj.getAttribute("title");
			if ($$.browser.IE){
				pkgEle.startCityDiv.innerHTML=pkgEle.startCityDivBak.innerHTML;
				pkgEle.startCity.blur();
			}
			pkgEle.destCity.init();
		}else
			pkgEle.startCity.blurFlag=true;
	});
	pkgEle.startCityDivBak=pkgEle.startCityDiv.cloneNode(true);
	pkgEle.destCityDiv.$r("mousedown",function(e){
		e=$fixE(e);
		if (e.$target.tagName=="A"){
			pkgEle.destCity.value=e.$target.innerHTML.trim();
			var dt = e.$target.getAttribute("dt");
			var dc = e.$target.getAttribute("dc");
			var di = e.$target.getAttribute("di");
			if(dt!=null&&dc!=null){
				pkgEle.destCity.setAttribute("dt" , dt);
				pkgEle.destCity.setAttribute("dc" , dc);
				pkgEle.destCity.setAttribute("di" , di);
			}
			if ($$.browser.IE){
				pkgEle.destCityDiv.innerHTML=pkgEle.destCityDivBak.innerHTML;
				pkgEle.destCity.blur();
			}
			pkgEle.destCityDiv.style.display="none";
		}
		pkgEle.destCity.blurFlag=true;
	});
	pkgEle.destCityDivBak=pkgEle.destCityDiv.cloneNode(true);
	//初始化出发地code
	if (pkgEle.startCityCode.value){
		pkgEle.startCity.value=$$.module.pkgSearch.pkgStartCityHash[pkgEle.startCityCode.value]||"";
		pkgEle.destCity.init();
	}
	//初始化目的地code
	if (!pkgEle.destCity.value.trim()&&pkgEle.destCityCode.value&&pkgEle.destCityType.value&&pkgEle.destCityId.value){
		(function(){
			if (!pkgEle.destCity.module.address){
				setTimeout(arguments.callee,500);
				return;
			}
			var strTmp=pkgEle.startCity.value.trim();
			var source=$$.module.address.source[strTmp?"pkg_"+strTmp:"pkgAll"];
			var re=new RegExp("@([^\\|]*)\\|([^\\|]*)\\|"+pkgEle.destCityId.value+"\\|"+pkgEle.destCityCode.value+"\\|"+pkgEle.destCityType.value+"[\\|@]","i");
			var arr=source.match(re);
			if (arr){
				pkgEle.destCity.value=arr[2]||arr[1];
				pkgEle.destCity.module.notice.check();
			}
		})();
	}
	//提交校验
	pkgEle.form.onsubmit=function(){
		var flag={destCityCode:pkgDestFlag.destCityCode,destCityType:pkgDestFlag.destCityType,destCityId:pkgDestFlag.destCityId};
		pkgDestFlag={destCityCode:null,destCityType:null,destCityId:null};
		var strTmp;
		//出发地校验
		strTmp=pkgEle.startCity.value.trim();
		if (!strTmp){
			$alert(pkgEle.startCity,$s2t("请选择您的出发地"));
			return false;
		}
		var code=$$.module.pkgSearch.pkgStartCityHash[strTmp];
		if (!code){
			$alert(pkgEle.startCity,$s2t("请选择正确的出发地"));
			return false;
		}
		pkgEle.startCityCode.value=code;
		//目的地处理
		var source=$$.module.address.source["pkg_"+strTmp];
		strTmp=pkgEle.destCity.isNull&&pkgEle.destCity.isNull()?"":pkgEle.destCity.value.trim();
		if (!strTmp)
			pkgEle.destCityCode.value=pkgEle.destCityType.value=pkgEle.destCityId.value="";
		else{
			if(pkgEle.destCity.getAttribute("dt")!=null&&pkgEle.destCity.getAttribute("dc")!=null&&pkgEle.destCity.getAttribute("di")!=null){
				pkgEle.destCityCode.value=pkgEle.destCity.getAttribute("dc");
				pkgEle.destCityType.value=pkgEle.destCity.getAttribute("dt");
				pkgEle.destCityId.value=pkgEle.destCity.getAttribute("di");
			}else{
				strTmp=strTmp.replace(/([\(\)\\\[\]\.\+\?\*\|\^\$])/gi,"\\$1").replace(/@|\|/gi,"");
				var re=new RegExp("@("+strTmp+"\\|\\||[^\\|]*\\|"+strTmp+"\\|)[^@]+","i");
				var arr=source.match(re);
				if (!arr&&/^[a-z]{2,}$/.test(strTmp)){
					re=new RegExp("@[^@]+\\|"+strTmp+"(\\|[^@]*)?(?=@)","i");
					arr=source.match(re);
				}
				if (arr){
					var str=arr.toString().split("|");
					pkgEle.destCityCode.value=str[3];
					pkgEle.destCityType.value=str[4];
					pkgEle.destCityId.value=str[2];
				}else
					pkgEle.destCityCode.value=pkgEle.destCityType.value=pkgEle.destCityId.value="";
			}
		}
		//强制填写destCityCode,destCityType,destCityId
		if (flag.destCityCode)
			pkgEle.destCityCode.value=flag.destCityCode;
		if (flag.destCityType)
			pkgEle.destCityType.value=flag.destCityType;
		if (flag.destCityId)
			pkgEle.destCityId.value=flag.destCityId;
		//目的地校验
		strTmp=pkgEle.destCity.isNull&&pkgEle.destCity.isNull()?"":pkgEle.destCity.value.trim();
		if (!strTmp){
			$alert(pkgEle.destCity,$s2t("请输入目的地"));
			return false;
		}
		return true;
	};
	//gosearch
	_.goSearch=function(obj,destCityType,destCityCode){
		var str=(obj.innerHTML||obj||"").toString().trim();
		if (!str)
			return;
		pkgDestFlag.destCityCode=destCityCode||null;
		pkgDestFlag.destCityType=destCityType||null;
		pkgEle.destCity.value=str;
		if (pkgEle.destCity.module.notice)
			pkgEle.destCity.module.notice.check();
		if (pkgEle.submitBtn.click)
			pkgEle.submitBtn.click();
		else{
			var evt=__.createEvent("MouseEvents");
			evt.initMouseEvent("click",true,true,_,0,0,0,0,0,false,false,false,false,0,null);
			pkgEle.submitBtn.dispatchEvent(evt);
		}
	};
	
	/** 度假搜索火车票 **/
	_.searchTrain = function(obj){
		var trainUrl = "", 
			  startCityPY= "Shanghai",
			  mainUrl = "vacations.ctrip.com";
		if($("vacationUrl") && $("vacationUrl").value!=""){
			mainUrl = $("vacationUrl").value;
		}
		var links = $("pkgStartCityDiv").$("a");
		for(var i=0,l=links.length; i<l; i++){
			if(links[i].innerHTML == $s2t(pkgEle.startCity.value)){
				startCityPY = links[i].getAttribute("title");
				break;
			}
		}
		
		trainUrl = "http://"+mainUrl+"/booking/PkgStore--startcity--"+startCityPY+"---dest--huoche.html";
		pkgEle.destCity.value = $s2t(obj.innerHTML);
		if (pkgEle.destCity.module.notice)
			pkgEle.destCity.module.notice.check();
		window.location.href = trainUrl;
	}
});

//保存tab状态
$r("beforeunload",function(){
	var searchBoxUl=$("searchBoxUl");
	if (searchBoxUl&&searchBoxUl.module.tab)
		$pageValue.set("module_tab_searchBoxUl",searchBoxUl.module.tab.index);
});
$r("domReady",function(){
	var searchBoxUl=$("searchBoxUl");
	var index=$pageValue.get("module_tab_searchBoxUl");
	if (index&&searchBoxUl&&searchBoxUl.module.tab)
		searchBoxUl.module.tab.select(index);
});


//QuickSearchInit
function quickSearchInit(){
	var form=__.forms["sform"];
	if (!form)
		return;
	var input=$(form["query"]);
	$d(input);
	form.onsubmit=function(){
		if (input.isNull()){
			$alert(input,$s2t("请输入快搜内容"),false,"tl","bl");
			return false;
		}
	};
}

/**
 * @description 世博滚动新闻
 * @author      Amio.Jin@ctrip.com
 * @note        滚动播出5条世博新闻（10秒上下翻滚播出）
 *              配置文件：http://pages.ctrip.com/webhome/chs/home/expo2010.txt
 */

var scrollText = function(parentEl,args){
	var config = $extend({
		fps: 50,
		duration: 500,
		height: 25,
		callback: function(){}
	}, args || {});

	function getFirstChild(parent){
		var t = parent.firstChild;
		while (t.nodeType != 1) {
			t = t.nextSibling;
		}
		return t;
	}

	var startTime = new Date(),
		currentMarginTop = 0,
		timeStep = Math.round(1000 / config.fps),
		timeLeft = config.duration,
		animTarget = getFirstChild(parentEl);

	function anim(){
		timeLeft = new Date() - startTime;
		if(timeLeft < config.duration){
			// animating
			currentMarginTop = config.height * ( 0 - timeLeft / config.duration);
			timeLeft += timeStep;
			animTarget.style.marginTop = Math.round(currentMarginTop) + "px";
		}else{
			// animation stoped
			clearInterval(timer);
			var animParent = animTarget.parentNode;
			animParent.appendChild(animParent.removeChild(animTarget));
			animTarget.style.marginTop = "0";
			animTarget = getFirstChild(parentEl);
			if(config.callback) setTimeout(config.callback, timeStep);
		}
	}

	var timer = setInterval(anim, timeStep);
};

$$.module.searchBox.init();

//首页酒店切换
//时间紧迫，先上一版原本的recommand的方法，todo..
$r("domready", function(){
	var pid=$("hotelRecommendContainer");
	var txt = $("hotelCityName");
	var div=$("hotelRecommendContainer").$("dl")[0];
	var panel=$("hotelRecommendPanel");
	var lastEl = nowEl = div.$("dd")[0];
	if (!div||!panel){return;}
	var cache={};
	cache[txt.value.trim()]=panel.innerHTML;
	var count=0;
	
	div.$r("mousedown",function(e){
		var src=$fixE(e).$target;
		if (src.tagName=="A"){
			showHotel(src);
			if ($$.browser.IE)
				setTimeout(function(){
					src.outerHTML+="";
				});
		}
	});
	
	function showHotel(src){
		var tmpCount=++count;
		nowEl = src.parentNode;
		if(lastEl == nowEl){
			return false;
		}else{
			lastEl.className = "area_nocurrent";
			nowEl.className = "area_current";
			lastEl = nowEl;
		}
		var name=src.innerHTML.trim();
		var page=src.getAttribute("page");
		
		//txt.value=name;
		
		var script = "if(typeof(SetLink)==\"function\"){SetLink(setlink_isbig5, '"+pid+"');}";
		
		if (name in cache){
			panel.innerHTML=cache[name];
			eval(script);
		}else{
			panel.innerHTML="<div class=\"pic_loading\"><\/div>";
			$ajax(page,null,function(str){
				if (count==tmpCount&&str!==false){
					cache[name]=panel.innerHTML=str;
					eval(script);
				}
			});
		}
	}
});


var $indexInitLoaded = true;

/** 酒店抽奖活动 **/
var showSales = function(target, num){
	this.init(target, num);
}

showSales.prototype = {
	init: function(target , num){
		if(!target || num == 0){return;}
		var _this = this;
		this.target = target;
		this.num = num;
		this.ps = this.target.$("p") , this.num = parseInt(this.num);
		this.eachHeight = this.ps[0].offsetHeight*this.num; //每次要跳转的高度
		this.realHeight = this.target.offsetHeight; //实际高度
		this.config = {"fps":100};
		for(var i=0,l=num; i<l; i++){
			this.target.appendChild(this.ps[i].cloneNode(true));
		}
		this.startTimer();
	},
	
	startTimer: function(){
		var _this = this;
		this.timer = setInterval(function(){
			_this.current = parseInt(_this.target.style.top.replace("px","")) || 0;
			if(_this.realHeight == Math.abs(_this.current)){
				_this.current = _this.target.style.top = 0;
			}
			_this.props = {
				"top":[_this.current, _this.current-_this.eachHeight]
			};
			$animate(_this.target, _this.props, _this.config);
		}, 3000);
	},
	
	stopTimer: function(){
		var _this = this;
		clearInterval(_this.timer);
	}
}

$r("domready", function(){
	window.leftScrollFlag = ($("saleList1").$("p").length >3);
	try{
		if(leftScrollFlag){ //左侧大于三条才滚动
			window.mySaleList1 = new showSales($("saleList1"), 3);
			$("saleList1").$r("mouseover", stopScroll);
			$("saleList1").$r("mouseout", startScroll);
		}
		window.mySaleList2 = new showSales($("saleList2"), 2);
		$("saleList2").$r("mouseover", stopScroll);
		$("saleList2").$r("mouseout", startScroll);
	}catch(e){}
	
});

function stopScroll(){
	if(leftScrollFlag){mySaleList1.stopTimer();}
	mySaleList2.stopTimer();
}

function startScroll(){
	if(leftScrollFlag){ mySaleList1.startTimer();} 
	mySaleList2.startTimer();
}




