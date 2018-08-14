<?php
 
include_once("./conex.php");
 
$sql = mysql_query("SELECT count(uppercuts_enviados) FROM `usuarios`");
$act = mysql_fetch_assoc($sql);
 session_start();
if($act >= 0){
        mysql_query("UPDATE usuarios SET nivel_ring = '0' where uppercuts_enviados >='0'    ") or die(mysql_error());
        }
/* Actualiza todos los usuarios al nivel_ring 0 (rojo) si el usuario tiene una cantidad superior es mayor o igual a 0. Si hay un error
en la consulta mysql lo mostrará en la página (dudo sobre si es uppercuts, la cantidad de uppercuts enviados). */
 
if($act >= 5){
        mysql_query("UPDATE usuarios SET nivel_ring = '1' where uppercuts_enviados >='5'    ") or die(mysql_error());
        }
 
if($act >= 20){
        mysql_query("UPDATE usuarios SET nivel_ring = '2' where uppercuts_enviados >='20'  ") or die(mysql_error());
        }
 
if($act >= 50){
        mysql_query("UPDATE usuarios SET nivel_ring = '3' where uppercuts_enviados >='50'    ") or die(mysql_error());
        }
       
if($act >= 100){
        mysql_query("UPDATE usuarios SET nivel_ring = '4' where uppercuts_enviados >='100'    ") or die(mysql_error());
        }
       
if($act >= 200){
        mysql_query("UPDATE usuarios SET nivel_ring = '5' where uppercuts_enviados >='200'    ") or die(mysql_error());
        }
               
if($act >= 500){
        mysql_query("UPDATE usuarios SET nivel_ring = '6' where uppercuts_enviados >='500'    ") or die(mysql_error());
        }
               
if($act >= 1000){
        mysql_query("UPDATE usuarios SET nivel_ring = '7' where uppercuts_enviados >='1000'    ") or die(mysql_error());
        }
               
if($act >= 1900){
        mysql_query("UPDATE usuarios SET nivel_ring = '8' where uppercuts_enviados >='1800'    ") or die(mysql_error());
        }
               
if($act >= 3000){
        mysql_query("UPDATE usuarios SET nivel_ring = '9' where uppercuts_enviados >='3000'    ")  or die(mysql_error());
       
        ;}
       
?>