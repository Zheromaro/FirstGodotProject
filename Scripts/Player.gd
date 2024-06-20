extends CharacterBody2D


var gravity: int = ProjectSettings.get_setting("physics/2d/default_gravity")
@export var SPEED = 130.0
@export var JUMP_VELOCITY = -300.0
@onready var animated_sprite: AnimatedSprite2D = $AnimatedSprite2D


func _physics_process(delta: float) -> void:
	#Gravity
	if not is_on_floor():
		velocity.y += gravity * delta
	
	#Jump
	if Input.is_action_just_pressed("Jump") and is_on_floor():
		velocity.y = JUMP_VELOCITY
	
	#Move
	var direction := Input.get_axis("Left", "Right")
	if direction:
		velocity.x = direction * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
	
	# Sprite
	if direction > 0:
		animated_sprite.flip_h = false
	elif direction < 0:
		animated_sprite.flip_h = true
	
	if is_on_floor():
		if direction:
			velocity.x = direction * SPEED
			animated_sprite.play("run")
		else:
			velocity.x = move_toward(velocity.x, 0, SPEED)
			animated_sprite.play("idel")
	else:
		animated_sprite.play("jump")
	
	move_and_slide()
