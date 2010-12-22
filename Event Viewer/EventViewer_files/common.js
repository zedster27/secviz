//Scroll
var Message=" CÔNG TY MÁY TÍNH - TRUYỀN THÔNG - ĐIỀU KHIỂN 3C";
		var place=1;
		function scrollIn() {
		window.status=Message.substring(0, place);
		if (place >= Message.length) {
			place=1;
			window.setTimeout("scrollOut()",1000);
		} else {
			place++;
			window.setTimeout("scrollIn()",50);
		}
		}
		function scrollOut() {
		window.status=Message.substring(place, Message.length);
		if (place >= Message.length) {
			place=1;
			window.setTimeout("scrollIn()", 200);
		} else {
			place++;
			window.setTimeout("scrollOut()", 100);
		}
		}
		scrollIn();

//end Scroll
PositionX = 100;
PositionY = 100;

// Set these value approximately 20 pixels greater than the
// size of the largest image to be used (needed for Netscape)

defaultWidth  = 500;
defaultHeight = 500;

// Set autoclose true to have the window close automatically
// Set autoclose false to allow multiple popup windows

var AutoClose = true;

// Do not edit below this line...
// ================================
if (parseInt(navigator.appVersion.charAt(0))>=4){
var isNN=(navigator.appName=="Netscape")?1:0;
var isIE=(navigator.appName.indexOf("Microsoft")!=-1)?1:0;}
var optNN='scrollbars=no,width='+defaultWidth+',height='+defaultHeight+',left='+PositionX+',top='+PositionY;
var optIE='scrollbars=no,width=500,height=500,left='+PositionX+',top='+PositionY;
function popImage(imageURL,imageTitle){
	if (isNN){imgWin=window.open('about:blank','',optNN);}
	if (isIE){imgWin=window.open('about:blank','',optIE);}
	with (imgWin.document){
		writeln('<html><head><title>Loading...</title><style>body{margin:0px;}</style>');
		writeln('<sc'+'ript>');
		writeln('var isNN,isIE;');
		writeln('if (parseInt(navigator.appVersion.charAt(0))>=4){');
		writeln('isNN=(navigator.appName=="Netscape")?1:0;');
		writeln('isIE=(navigator.appName.indexOf("Microsoft")!=-1)?1:0;}');
		writeln('function reSizeToImage(){');
		writeln('if (isIE){');
		writeln('temp=navigator.appVersion.split("MSIE"); version=parseFloat(temp[1])');
		writeln('if (version>=7.0){ window.resizeTo(250,250); height=250-(document.body.clientHeight-document.images[0].height);');
		writeln('width=250-(document.body.clientWidth-document.images[0].width);}');
		writeln('else{ window.resizeTo(100,100); height=100-(document.body.clientHeight-document.images[0].height);');
		writeln('width=100-(document.body.clientWidth-document.images[0].width);}');
		writeln('window.resizeTo(width,height);}');
		writeln('if (isNN){');       
		writeln('window.innerWidth=document.images["George"].width;');
		writeln('window.innerHeight=document.images["George"].height;}}');
		writeln('function doTitle(){document.title="'+imageTitle+'";}');
		writeln('</sc'+'ript>');
		if (!AutoClose) writeln('</head><body bgcolor=000000 scroll="no" onload="reSizeToImage();doTitle();self.focus()">')
		else writeln('</head><body bgcolor=000000 scroll="no" onload="reSizeToImage();doTitle();self.focus()" onblur="self.close()">');
		writeln('<a href="" onClick="window.close(); return false;"><img align=center name="George" alt="Đóng lại" src='+imageURL+' border="0" style="display:block"></a></body></html>');
		close();		
	}
}
/*function OnCmdSubmitClick(LocationRequest, nameRad, Width, Height)
{
	var chasm = screen.availWidth;
	var mount = screen.availHeight;
	window.open('','poll','left='+ (chasm - Width - 10)* .5+',top='+(mount - Height - 30)* .5+',width='+ Width +',height='+ Height +',toolbar=1,resizable=0');	
	document.forms[0].target='poll';
	document.forms[0].action='/Poll.aspx';
	document.forms[0].__VIEWSTATE.name = 'NOVIEWSTATE';
	document.forms[0].method = "post";
   	document.forms[0].submit();	
}*/
function OnCmdSubmitClick(LocationRequest, nameRad, Width, Height)
{
	var chasm = screen.availWidth;
	var mount = screen.availHeight;
	var bIsChecked = false;
	var r = document.getElementsByName(nameRad);
		for(i=0;i<r.length;i++)
		{
			if( r[i].checked )
			{
			window.open('/UserControls/Poll/default.aspx?results='+ r[i].value +'&LID='+LocationRequest,'Poll','left='+ (chasm - Width - 10)* .5+',top='+(mount - Height - 30)* .5+',width='+ Width +',height='+ Height +',toolbar=1,resizable=0');
			bIsChecked = true;
			break;
			}
		}
	if(!bIsChecked ) alert("Hãy chọn ít nhất một mục để bình chọn!");
	return bIsChecked;
}

function OnCmdViewClick(LocationRequest, Width, Height)
{
	var chasm = screen.availWidth;
	var mount = screen.availHeight;
	window.open('/UserControls/Poll/default.aspx?results=-1&LID='+LocationRequest,'Poll','locationbar=no,left='+ (chasm - Width - 10)* .5+',top='+(mount - Height - 30)* .5+',width='+ Width +',height='+ Height +',toolbar=1,resizable=0');
}


