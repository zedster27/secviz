

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
