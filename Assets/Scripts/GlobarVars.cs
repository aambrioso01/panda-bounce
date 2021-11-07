using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobarVars : MonoBehaviour
{
    // int mapping for layers
    public static int LAYER_DEFAULT = 0;
    public static int LAYER_TRANSPARENT_FX = 1;
    public static int LAYER_IGNORE_RAYCAST = 2;
    public static int LAYER_WATER = 4;
    public static int LAYER_UI = 5;
    public static int LAYER_PLAYER_PROJECTILES = 6;
    public static int LAYER_PLAYER_SHIP = 7;
    public static int LAYER_ENEMIES = 8;

    // standard vectors
    public static Vector3 VECTOR_UP = new Vector3(0,1,0);
    public static Vector3 VECTOR_UP_LEFT = new Vector3(-1,1,0);
    public static Vector3 VECTOR_UP_RIGHT = new Vector3(1,1,0);
    public static Vector3 VECTOR_DOWN = new Vector3(0,-1,0);
    public static Vector3 VECTOR_DOWN_LEFT = new Vector3(-1,-1,0);
    public static Vector3 VECTOR_DOWN_RIGHT = new Vector3(1,-1,0);
    public static Vector3 VECTOR_LEFT = new Vector3(-1,0,0);
    public static Vector3 VECTOR_RIGHT = new Vector3(1,1,0);

    // projectile settings

    /* rocket
    public static float PROJECTILE_ROCKET_MOVE_SPEED = 300;
    public static float PROJECTILE_ROCKET_BULLET_DAMAGE = 50;
    public static int PROJECTILE_ROCKET_MAX_NUM_OF_ENEMIES = 3;
    public static Vector3[] PROJECTILE_ROCKET_VECTORS = new Vector3[] {
        VECTOR_UP,
    };
    */ 

    /* energy ball
    public static float PROJECTILE_ENERGYBALL_MOVE_SPEED = 200;
    public static float PROJECTILE_ENERGYBALL_BULLET_DAMAGE = 34;
    public static int PROJECTILE_ENERGYBALL_MAX_NUM_OF_ENEMIES = 1;
    public static Vector3[] PROJECTILE_ENERGYBALL_VECTORS = new Vector3[] {
        VECTOR_UP,
        VECTOR_UP_LEFT,
        VECTOR_UP_RIGHT
    }; 
    */
    
    /* objects to store each projectiles properties
    public static ProjectileSettings PROJECTILE_ROCKET = new ProjectileSettings(
        PROJECTILE_ROCKET_VECTORS,
        PROJECTILE_ROCKET_MOVE_SPEED,
        PROJECTILE_ROCKET_BULLET_DAMAGE,
        PROJECTILE_ROCKET_MAX_NUM_OF_ENEMIES
    );

    public static ProjectileSettings PROJECTILE_ENERGYBALL = new ProjectileSettings(
        PROJECTILE_ENERGYBALL_VECTORS,
        PROJECTILE_ENERGYBALL_MOVE_SPEED,
        PROJECTILE_ENERGYBALL_BULLET_DAMAGE,
        PROJECTILE_ENERGYBALL_MAX_NUM_OF_ENEMIES
    );
    */
}
