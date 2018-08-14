<?php
include('config.php');
if(!isset($_SESSION[usuario]))
{
echo '
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>BoomBang Retro Control Panel</title>
<style type="text/css">
<!--
input {
border: 0px;
font: VAGRoundedBT, Arial, Helvetica, sans-serif;
font-size: 18px;
}
body {
	background-image: url(http://images.alphacoders.com/211/211451.jpg);
}
.Estilo1 {
	font-family: Arial, Helvetica, sans-serif;
	font-weight: bold;
	color: #666600;
}
.box {
-webkit-border-radius: 7px;
background: #669900;
padding: 5px;
margin: 5px;
}
.Estilo2 {
	font-family: Arial, Helvetica, sans-serif;
	font-weight: bold;
	color: #FFFFFF;
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
-->
</style></head>

<body>
<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td valign="middle"><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><div align="center">
      <table width="450" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="450" height="200" valign="top" background="http://boombangretro.servegame.com/images/inic_png_NkxUZgGYoxC.png"><div align="center">
		  <form action="conectarse.php" method="post">
            <table width="450" height="185" border="0" cellpadding="0" cellspacing="5">
              <tr>
                <td height="47">&nbsp;</td>
              </tr>
              <tr>
                <td height="30"><div align="center">
<center><font color="#FFFFFF">Username / Usuario</font> </center>    <input name="usuario" type="text" size="45" />
                  </div></td>
              </tr>
              <tr>
                <td height="19">&nbsp;</td>
              </tr>
              <tr>
                <td height="31"><div align="center">
<center><font color="#FFFFFF">Password / Contrasena</font> </center> <input name="pass" type="password" size="45" />
                  </div></td>
              </tr>
              <tr>
                <td><div align="center"style="margin-top: 5px;">
                  <input type="submit" name="conectar" value="       Login / Inicia Sesion" />
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
echo '
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<style type="text/css">
<!--
body {
	background-image: url(http://images.alphacoders.com/211/211451.jpg);
}
.Estilo1 {
	font-family: Arial, Helvetica, sans-serif;
	font-weight: bold;
	color: #666600;
}
.box {
-webkit-border-radius: 7px;
background: #669900;
padding: 5px;
margin: 5px;
}
.Estilo2 {
	font-family: Arial, Helvetica, sans-serif;
	font-weight: bold;
	color: #FFFFFF;
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
-->
</style></head>

<body>
<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td valign="middle"><div align="center">
      <table width="556" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="556" height="450" valign="top" background="http://boombangretro.servegame.com/images/home_log_png_etAwzSQf9Dfh.png"><div align="center">
            <table width="100%" border="0" cellspacing="5" cellpadding="0">
              <tr>
                <td height="37"><div align="center" class="Estilo1">Account</div></td>
              </tr>
              <tr>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td><div align="left" class="box"><form action="userchange.php" method="post">
                  <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                      <td width="36%" class="Estilo2">Characters: </td>
                      <td width="64%"><select name="keko">
	<option value="12" selected="selected">Ninja</option>
	<option value="3">Keko</option>
	<option value="7">Keka</option>
	                                    </select></td>
                    </tr>
                    <tr>
                      <td class="Estilo2">Skin Color:					  </td>
                      <td><select name="c_piel">
	<option value="CC9966" selected="selected">Dark Tan</option>
	<option value="856444">Black Tan</option>
	<option value="FFC589">Tan</option>
	<option value="66CCCC">Light Blue</option>
	<option value="013ADF">Blue</option>
	<option value="000099">Dark Blue</option>
	<option value="FF0033">Light Red</option>
	<option value="FF0000">Red</option>
	<option value="990000">Dark Red</option>
	<option value="FF00CC">Light Pink</option>
	<option value="FF00FF">Pink</option>
	<option value="FF0080">Dark Pink</option>
	<option value="00FF00">Light Green</option>
	<option value="5FB404">Green</option>
	<option value="336600">Dark Green</option>
	<option value="FFFF66">Light Yellow</option>
	<option value="FFFF00">Yellow</option>
	<option value="CCCC00">Dark Yellow</option>
	<option value="CC99CC">Light Purple</option>
	<option value="6A0888">Purple</option>
	<option value="990099">Dark Purple</option>
	<option value="996633">Light Brown</option>
	<option value="61210B">Brown</option>
	<option value="663333">Dark Brown</option>
	<option value="CCCCCC">Light Grey</option>
	<option value="6E6E6E">Grey</option>
	<option value="FFFFFF">White</option>
	<option value="404040">Dark Grey</option>
	<option value="FFCC33">Peach</option>
	<option value="FF9900">Light Orange</option>
	<option value="FF8000">Orange</option>
	<option value="CC9900">Dark Orange</option>
	<option value="FFCC00">Gold</option>
		                  </select></td>
                    </tr>
                    <tr>
                      <td class="Estilo2">Color of Eyes: </td>
                      <td><select name="c_ojos">
	<option value="000000" selected="selected">Black</option>
	<option value="856444">Black Tan</option>
	<option value="FFC589">Tan</option>
	<option value="CC9966">Dark Tan</option>
	<option value="66CCCC">Light Blue</option>
	<option value="013ADF">Blue</option>
	<option value="000099">Dark Blue</option>
	<option value="FF0033">Light Red</option>
	<option value="FF0000">Red</option>
	<option value="990000">Dark Red</option>
	<option value="FF00CC">Light Pink</option>
	<option value="FF00FF">Pink</option>
	<option value="FF0080">Dark Pink</option>
	<option value="00FF00">Light Green</option>
	<option value="5FB404">Green</option>
	<option value="336600">Dark Green</option>
	<option value="FFFF66">Light Yellow</option>
	<option value="FFFF00">Yellow</option>
	<option value="CCCC00">Dark Yellow</option>
	<option value="CC99CC">Light Purple</option>
	<option value="6A0888">Purple</option>
	<option value="990099">Dark Purple</option>
	<option value="996633">Light Brown</option>
	<option value="61210B">Brown</option>
	<option value="663333">Dark Brown</option>
	<option value="CCCCCC">Light Grey</option>
	<option value="FFFFFF">White</option>
	<option value="6E6E6E">Grey</option>
	<option value="404040">Dark Grey</option>
	<option value="FFCC33">Peach</option>
	<option value="FF9900">Light Orange</option>
	<option value="FF8000">Orange</option>
	<option value="CC9900">Dark Orange</option>
	<option value="FFCC00">Gold</option>
		                                    </select></td>
                    </tr>
                    <tr>
                      <td class="Estilo2">Suit Color:</td>
                      <td><select name="c_traje">
	<option value="000000" selected="selected">Black</option>
	<option value="856444">Black Tan</option>
	<option value="FFC589">Tan</option>
	<option value="CC9966">Dark Tan</option>
	<option value="66CCCC">Light Blue</option>
	<option value="013ADF">Blue</option>
	<option value="000099">Dark Blue</option>
	<option value="FF0033">Light Red</option>
	<option value="FF0000">Red</option>
	<option value="990000">Dark Red</option>
	<option value="FF00CC">Light Pink</option>
	<option value="FF00FF">Pink</option>
	<option value="FF0080">Dark Pink</option>
	<option value="00FF00">Light Green</option>
	<option value="5FB404">Green</option>
	<option value="336600">Dark Green</option>
	<option value="FFFF66">Light Yellow</option>
	<option value="FFFF00">Yellow</option>
	<option value="CCCC00">Dark Yellow</option>
	<option value="CC99CC">Light Purple</option>
	<option value="6A0888">Purple</option>
	<option value="990099">Dark Purple</option>
	<option value="996633">Light Brown</option>
	<option value="61210B">Brown</option>
	<option value="663333">Dark Brown</option>
	<option value="CCCCCC">Light Grey</option>
	<option value="FFFFFF">White</option>
	<option value="6E6E6E">Grey</option>
	<option value="404040">Dark Grey</option>
	<option value="FFCC33">Peach</option>
	<option value="FF9900">Light Orange</option>
	<option value="FF8000">Orange</option>
	<option value="CC9900">Dark Orange</option>
	<option value="FFCC00">Gold</option>
			                  </select></td>
                    </tr>
                    <tr>
                      <td class="Estilo2"><span class="Estilo2">Belt Color:</span></td>
                      <td><select name="c_cinta">
	<option value="000000" selected="selected">Black</option>
	<option value="856444">Black Tan</option>
	<option value="FFC589">Tan</option>
	<option value="CC9966">Dark Tan</option>
	<option value="66CCCC">Light Blue</option>
	<option value="013ADF">Blue</option>
	<option value="000099">Dark Blue</option>
	<option value="FF0033">Light Red</option>
	<option value="FF0000">Red</option>
	<option value="990000">Dark Red</option>
	<option value="FF00CC">Light Pink</option>
	<option value="FF00FF">Pink</option>
	<option value="FF0080">Dark Pink</option>
	<option value="00FF00">Light Green</option>
	<option value="5FB404">Green</option>
	<option value="336600">Dark Green</option>
	<option value="FFFF66">Light Yellow</option>
	<option value="FFFF00">Yellow</option>
	<option value="CCCC00">Dark Yellow</option>
	<option value="CC99CC">Light Purple</option>
	<option value="6A0888">Purple</option>
	<option value="990099">Dark Purple</option>
	<option value="996633">Light Brown</option>
	<option value="61210B">Brown</option>
	<option value="663333">Dark Brown</option>
	<option value="CCCCCC">Light Grey</option>
	<option value="6E6E6E">Grey</option>
	<option value="FFFFFF">White</option>
	<option value="404040">Dark Grey</option>
	<option value="FFCC33">Peach</option>
	<option value="FF9900">Light Orange</option>
	<option value="FF8000">Orange</option>
	<option value="CC9900">Dark Orange</option>
	<option value="FFCC00">Gold</option>
	                  </select></td>
                    </tr>
                  </table>
                  <div align="center">
                    <input type="submit" name="guardar" value="Change">
                  </div></form><br />
                </div>
				<center>';
								echo '
<div>
								  <a href="edityears.php" class="Estilo2">Edit Months</a><br>
								  <a href="changepassword.php" class="Estilo2">Change Password</a><br>
								  <a href="logout.php" class="Estilo2">Log Out</a>



							
				  </center>
				</td>
              </tr>
            </table>
          </div>
		   </td>
        </tr>
      </table>
    </div></td>
  </tr>
</table>
</body>
</html>
';
}
if($_SESSION['moderador'] >= 1) {
echo '<center><a href="moderador.php" class="Estilo2">Moderation Panel</a></center><br>';
}
if($_SESSION['vip'] <= 1) {
echo '<center><a href="vip.php" class="Estilo2">VIP Panel</a></center>';
}
?>