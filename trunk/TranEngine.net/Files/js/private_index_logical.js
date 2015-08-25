//繁体版ajax的url转换函数
var setlink_isbig5 = $("isbig5").value || "www.ctrip.com";
//简体链接转为繁体链接
function SetLink(isbig5,panelid)
{	
	var strvaction="vacations.";
	var strhotel="hotels.";
	var strflight="flights."
	var strpages="pages.";
	var strcards="cards.";
	var nodes;
	var dd;
		if(arguments.length==2&&$(panelid)){
		  nodes = $(panelid).$("a");
		  dd = $(panelid).$("dd");
		}else{
		  nodes = __.$("a");
		  dd = __.$("dd");
		}

	if (isbig5.toString().indexOf("big5.")>=0)
	{
		for (var i=0;i<nodes.length;i++)
		{	if (nodes[i].toString().indexOf(".big5.")<0)
			{
				if(nodes[i].toString().indexOf(strvaction)>=0){
					nodes[i].href=nodes[i].toString().replace(strvaction,"vacations.big5.");
				}
				if(nodes[i].toString().indexOf(strhotel)>=0)
				{
					nodes[i].href=nodes[i].toString().replace(strhotel,"hotels.big5.");
				}
				if(nodes[i].toString().indexOf(strflight)>=0)
				{
					nodes[i].href=nodes[i].toString().replace(strflight,"flights.big5.");
				}
				if(nodes[i].toString().indexOf(strpages)>=0)
				{
					nodes[i].href=nodes[i].toString().replace(strpages,"pages.big5.");
				}
				if(nodes[i].toString().indexOf(strcards)>=0)
				{
					nodes[i].href=nodes[i].toString().replace(strcards,"cards.big5.");
				}	
			}
		}
		
		for (var i=0;i<dd.length;i++)
		{
			if(dd[i].getAttribute("onclick")&&dd[i].getAttribute("onclick").toString().indexOf(strflight)>=0)
			{
				(function(){
					var url = dd[i].getAttribute("onclick").toString().split("'")[1].replace("'" , "").replace(strflight,"flights.big5.").trim();
					dd[i].onclick =function(){
						window.location = url;
					}
				})();
			}
		}
	}
	
	if (isbig5.toString().indexOf("ctrip.com.hk")>=0)
	{	
		for (var i=0;i<nodes.length;i++)
		{	if (nodes[i].toString().indexOf("ctrip.com.hk")<0)
			{
				if(nodes[i].toString().indexOf(strvaction)>=0)
				{
					nodes[i].href=nodes[i].toString().replace("ctrip.com","ctrip.com.hk");
				}
				if(nodes[i].toString().indexOf(strhotel)>=0)
				{
					nodes[i].href=nodes[i].toString().replace("ctrip.com","ctrip.com.hk");
				}
				if(nodes[i].toString().indexOf(strflight)>=0)
				{
					nodes[i].href=nodes[i].toString().replace("ctrip.com","ctrip.com.hk");
				}

			}
		}
		
		for (var i=0;i<dd.length;i++)
		{
			if(dd[i].getAttribute("onclick")&&dd[i].getAttribute("onclick").toString().indexOf(strflight)>=0)
			{
				(function(){
					var url = dd[i].getAttribute("onclick").toString().split("'")[1].replace("'" , "").replace("ctrip.com","ctrip.com.hk").trim();
					dd[i].onclick =function(){
						window.location = url;
						
					}
				})();
			}
		}
	}
	
}   
   
//关闭popup 
//
function closeremind(obj){
	   obj.parentNode.style.display = "none";
	   $$.cookie.expires = 2160;
	   $setCookie('closeremind',1);
	   $("hotelGroupIcon").style.display = "block";
}

$r("domready", function(){
	if(!$("hotelGroupTip")){return;}
	if($getCookie("closeremind") == 1){
		$("hotelGroupTip").style.display = "none";
		$("hotelGroupIcon").style.display = "block";
	}else{
		$("hotelGroupTip").style.display = "block";
	}
});

SetLink(setlink_isbig5,"!@");


//VIP特约商户数据和携程热点
$r("domready" , function(){
	var vipList = [] , arr = $("viplist").childNodes;
	for(var i=0,l=arr.length; i<l; i++){
		if(arr[i].nodeType == 1){
			vipList.push(arr[i]);
		}
	}
	vipList[0].style.display = "none";
	var vipListR=vipList[Math.floor(vipList.length*Math.random())];
	vipListR.style.display = "";
	
	var vhlists = $("vhlists").$("div");
	var vhlistsR=vhlists[Math.floor(vhlists.length*Math.random())];
	vhlistsR.style.display = "";
});

	

