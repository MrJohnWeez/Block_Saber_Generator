execute at @s run summon rabbit ~ ~ ~ {Tags:[noteRed,note,node,blockBeat,noteNeedsNodeRowID],NoGravity:1b,Silent:1b,NoAI:1B,DeathLootTable:"minecraft:air",DeathTime:19s,PersistenceRequired:1b,Variant:0,Age:-77777777,Team:NoCollide,ActiveEffects:[{Id:14b,Amplifier:0b,Duration:999999,ShowIcon:0b,ShowParticles:0b}]}
execute at @s run summon armor_stand ~ ~ ~ {Tags:[node,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,ArmorItems:[{},{},{},{Count:1b,id:"minecraft:red_glazed_terracotta"}],Marker:1b,Invulnerable:1b,Small:1b}
execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongUUID=SONGUUID}] run scoreboard players operation @e[type=rabbit,tag=noteNeedsNodeRowID] NodeRowID = @s NodeRowID
tag @e[type=rabbit,tag=noteNeedsNodeRowID] remove noteNeedsNodeRowID