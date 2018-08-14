<?php
$con = mysql_connect("localhost","root","Nodna@9087");
$db = mysql_select_db("boombang", $con);
@session_start();
@session_destroy(); 
Header("Location: index.php"); 

?>