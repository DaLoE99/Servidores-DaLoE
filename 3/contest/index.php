<center>
<p><p>
	<table border="3" width="600"  cellspacing="1" >
	<h2> <font="26">Concursos</size></h2>
	<td><center>Top</td>
	<td><center>Diamonds</center></td>
<tr>

<?php
include ("dbsettings.php"); 

	mysql_connect($dbhost, $dbuser, $dbpw) OR
	die("ERROR: Connection failed. ".mysql_error());		
	mysql_select_db($db) OR
	die("ERROR: DB allready open. ".mysql_error());
	$test = "SELECT * from rankings";
		$testquery = mysql_query($test);
			$num2 = mysql_num_rows($testquery);

			$i = 0;


	$acc = mysql_query("SELECT * from rankings order by puntos_1 desc limit 100");
	$aco = mysql_query("SELECT * from usuarios order by id desc");
	while($row = mysql_fetch_object($acc)) 
	{
		$id = $row->usuario;
		$mocos = $row->puntos_1;
		$i = $i + 1;
		
			 while($rou = mysql_fetch_object($aco))
			 {
			 	$di = $rou->id;
			 	$name = $rou->usuario;
			 }
			 $di = $id;

$a = "
	<td> <center>#$i</td>
	<td> <center>$id</td>
	<td> <center>$name</td>
	<td> <center>$mocos</td><tr>
";
echo $a;}
?>