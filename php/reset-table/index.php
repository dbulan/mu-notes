<?php

$filename = 'mux-dune.dat';

$file = file($filename);

$arr = [];
$i = 0; $j = 0;
foreach ($file as $key => $line)
{
	$line = trim($line);
	if($line <> 'x')
	{
		if(!isset($arr[$i])) $arr[$i] = [-1, -1, -1];
	
		$arr[$i][$j] = $line;
	}
	
	$i++;
	if($line == 'x') { $i = 0; $j++; }
}

//display_bless($arr);
display_legend($arr);

function display_bless($arr)
{
	$st = 0; $nd = 1;
	
	echo '$resetsvalue = [1 => // [Total Normal, Total MG/DL, Bonuses]';
	foreach($arr as $key => $val)
	{
		$a = $arr[$key];
		
		echo "<br>";
		echo "[".$a[$st].", ".$a[$nd].", ".($a[2] == '-' ? 0 : $a[2])."],";
	}
	echo "<br>[".$a[$st].", ".$a[$nd].", ".$a[2]."] //".count($arr);
	echo "<br>";
	echo "];";
}

function display_legend($arr)
{
	$st = 1; $nd = 0;
	
	echo 'return array(1 =>';
	foreach($arr as $key => $val)
	{
		$a = $arr[$key];
		
		echo "<br>";
		echo "[".$a[$st].", ".$a[$nd]."],";
	}
	echo "<br>";
	echo ");";


	echo '<br><br>';
	echo '$resetBonusesTable = [';
	$r = 1;
	foreach($arr as $key => $val)
	{
		if($val[2] <> '-') echo "'".$r."' => ".$val[2].", ";

		$r++;
	}
	echo '];';
}