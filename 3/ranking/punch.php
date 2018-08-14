<?php
error_reporting(0);
$con = mysql_connect("localhost","root","Nodna@9087");
$sql = mysql_select_db("boombang", $con);
session_start();
$uppers=mysql_query("SELECT * FROM usuarios order by uppercuts_enviados desc");

while($arrayranking=mysql_fetch_array($uppers)){

	echo $arrayranking['uppercuts_enviados']; 
	echo '<br/>';
	
}
?>