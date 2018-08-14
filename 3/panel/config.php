<?php
error_reporting(0);
$con = mysql_connect("localhost","root","Nodna@9087");
$sql = mysql_select_db("boombang", $con);
session_start();
$boardurl = 'boombangretro.zapto.org';
?>
