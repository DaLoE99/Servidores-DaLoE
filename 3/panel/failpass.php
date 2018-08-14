<?php
error_reporting(0);
$con = mysql_connect("localhost","root","Nodna@9087");
$db = mysql_select_db("boombang", $con);
session_start();
if(!isset($_SESSION[usuario])) 
{
echo '
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Conectarse al Panel</title>
<style type="text/css">
<!--
input {
border: 0px;
font: VAGRoundedBT, Arial, Helvetica, sans-serif;
font-size: 18px;
}
body {
	background-image: url(http://www.boombang.tv/images/body_bg.png);
}
.box {
-webkit-border-radius: 7px;
background: #669900;
padding: 5px;
margin: 5px;
}
a:link {
	color: #FFFFFF;
	text-decoration: none;
}
a:visited {
	text-decoration: none;
	color: #FFFFFF;
}
a:hover {
	text-decoration: none;
	color: #CC00FF;
}
a:active {
	text-decoration: none;
	color: #FFFFFF;
}
.Estilo3 {
	color: #FF0000;
	font-size: 18px;
	font-weight: bold;
	font-family: Arial, Helvetica, sans-serif;
}
-->
</style></head>

<body>
<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td valign="middle"><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><div align="center">
      <p class="Estilo3">Username not found or incorrect password</p>
      <table width="450" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="450" height="200" valign="top" background="http://www.boom-up.tk/images/5/inic_png_NkxUZgGYoxC.png"><div align="center">
		  <form action="conectarse.php" method="post">
            <table width="450" height="185" border="0" cellpadding="0" cellspacing="5">
              <tr>
                <td height="47">&nbsp;</td>
              </tr>
              <tr>
                <td height="30"><div align="center">
                      <input name="usuario" type="text" size="45" />
                  </div></td>
              </tr>
              <tr>
                <td height="19">&nbsp;</td>
              </tr>
              <tr>
                <td height="31"><div align="center">
                      <input name="pass" type="password" size="45" />
                  </div></td>
              </tr>
              <tr>
                <td><div align="center"style="margin-top: 5px;">
                  <input type="submit" name="conectar" value="       Log In      " />
                  </div></td>
              </tr>
            </table>
			</form>
          </div></td>
        </tr>
      </table>
    </div></td>
  </tr>
</table>
</body>
</html>

';
}else{
header("location: index.php");
}
?>
