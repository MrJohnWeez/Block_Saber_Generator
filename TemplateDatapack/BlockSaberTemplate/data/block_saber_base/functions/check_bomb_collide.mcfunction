execute if score @s NoteHealth < #CONST10 Const_10 run say -1 Hit Bomb
execute if score @s NoteHealth < #CONST10 Const_10 at @s run kill @e[type=armor_stand,distance=..0.3]
execute if score @s NoteHealth < #CONST10 Const_10 run kill @s
execute if score @s NoteHealth > #CONST10 Const_10 run say -1 Hit Bomb
execute if score @s NoteHealth > #CONST10 Const_10 at @s run kill @e[type=armor_stand,distance=..0.3]
execute if score @s NoteHealth > #CONST10 Const_10 run kill @s


execute at @e[type=armor_stand,tag=fakePlayerEyes,tag=blockBeat] if entity @s[distance=..0.3] run say -1 Hit Bomb
execute at @e[type=armor_stand,tag=fakePlayerEyes,tag=blockBeat] if entity @s[distance=..0.3] at @s run kill @e[type=armor_stand,tag=node,distance=..0.3]
execute at @e[type=armor_stand,tag=fakePlayerEyes,tag=blockBeat] if entity @s[distance=..0.3] run kill @s


#execute at @e[type=armor_stand,tag=playerOrgin] if entity @p[scores={SongUUID=1111,IsPlayerSneeking=1..}] run say sneeking!
