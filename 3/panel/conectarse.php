<?php
$con = mysql_connect("localhost","root","pagel1999");
$db = mysql_select_db("boombang", $con);
session_start();
if( ($_POST[nick] == ' ') or ($_POST[pass] == ' ') )
{
Header("Location: index.php"); 
}else{

$pass1 = $_POST['pass'];
// $pass = base64_encode(sha1($pass1, md5));
$pass = base64_encode(sha1($pass1, md5));

$usuarios=mysql_query("SELECT * FROM usuarios WHERE usuario='$_POST[usuario]' and password='$pass' ");
while($user_ok = mysql_fetch_assoc($usuarios)) 
{ 
$_SESSION['upper'] = $user_ok["nivel_ring"]; 
$_SESSION['usuario'] = $user_ok["usuario"]; 
$_SESSION['idusuario'] = $user_ok["id"]; 
$_SESSION['moderador'] = $user_ok["moderador"]; 
Header("Location: index.php"); 

}
Header("Location: failpass.php");
echo $pass;


} 
?>