execute at @e[type=armor_stand,tag=playerOrgin] if score @s NodeRowID = @p[scores={SongUUID=SONGUUID}] NodeRowID run function block_saber_base:note_was_correct