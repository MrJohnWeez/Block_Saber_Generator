execute if score @s NoteHealth < #CONST Const_3 run function _root_class:note_was_wrong
execute if entity @s[name="Blue Saber"] run function _root_class:note_was_wrong
execute at @e[type=armor_stand,tag=fakePlayerEyes,tag=blockBeat] if entity @s[distance=..0.21] run function _root_class:note_was_wrong