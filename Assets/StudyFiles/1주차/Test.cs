using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private double _hp = 10.0;
    public double Damage => 5.0;
    private void Start()
    {
        Debug.Log("몬스터 생성.");
    }

    public bool Damaged(double damage) 
    {
        _hp -= damage;
        return _hp < 0;
    }
}

enum WeaponType
{
    Sword, 
    Gun, 
    None
}


public class Player : MonoBehaviour
{
    private WeaponType Weapon = WeaponType.None;

    private double _hp = 100.0;
    void Start()
    {
        Debug.Log("플레이어 생성~!");
    }

    public bool Damage(Monster monster)
    {
        Debug.Log($"플레이어가 <unnamed>에게 데미지를 줌!");

        return Weapon switch
        {
            WeaponType.Gun => monster.Damaged(5.0),
            WeaponType.Sword => monster.Damaged(3.0),
            _ => false,
        };
    }

    public bool Damaged(Monster monster)
    {
        Debug.Log($"플레이어가 <unnamed>에게 {monster.Damage} 데미지를 받음!");
        _hp -= monster.Damage;
        
        return _hp < 0;
    }
}

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private Player _player;
    private readonly List<Monster> _monsters = new();
    public void Start()
    {
        Debug.Log("Starting");

        _player = gameObject.AddComponent<Player>();
        _monsters.Add(gameObject.AddComponent<Monster>());
        _monsters.Add(gameObject.AddComponent<Monster>());
        _monsters.Add(gameObject.AddComponent<Monster>());
        _monsters.Add(gameObject.AddComponent<Monster>());
        var count = 1000;
        while (count >= 0)
        {
            count -= 1;
            foreach (Monster mon in _monsters.Where(mon => _player.Damage(mon)))
            {
                _monsters.Remove(mon);
            }
            
            if (_monsters.Any(mon => _player.Damaged(mon)))
            {
                Debug.Log("플레이어의 패배..");
                break;
            }

            if (_monsters.Count != 0) continue;
            
            Debug.Log($"플레이어의 승리!");
            break;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
