using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureParticleManager : MonoBehaviour
{
    public static TextureParticleManager Instance;

    private MeshParticleSystem _meshParticleSystem;

    private List<Particle> _shellList;
    private List<Particle> _bloodList;

    private void Awake() {
        _meshParticleSystem = GetComponent<MeshParticleSystem>();
        Instance = this;
        _shellList = new List<Particle>();
        _bloodList = new List<Particle>();


    }

    private void Update(){
        for(int i =0; i< _bloodList.Count ;i++)
        {
            Particle p = _bloodList[i];
            p.UpdateParticle();
            if(p.IsComplete())
            {
                _bloodList.RemoveAt(i);
                i--;
            }
        }
        for(int i = 0; i < _shellList.Count ;i++)
        {
            Particle p = _shellList[i];
            p.UpdateParticle();
            if(p.IsComplete())
            {
                _shellList.RemoveAt(i);
                i--;
            }
        }
        
    }

    public void SpawnShell(Vector3 pos, Vector3 dir){
        int uvIndex = _meshParticleSystem.GetRandomShellIndex();
        float moveSpeed = Random.Range(1.5f,2.5f);
        Vector3 quadSize = new Vector3(0.15f,0.15f);

        float slowDownFactor = Random.Range(2f,2.5f);

        _shellList.Add(new Particle(pos, dir, _meshParticleSystem, quadSize, Random.Range(0,359f), uvIndex, moveSpeed, slowDownFactor,true));
    }

    
    public void SpawnBlood(Vector3 pos, Vector3 dir,float size){
        int uvIndex = _meshParticleSystem.GetRandomBloodIndex();
        float moveSpeed = Random.Range(1.5f,2.5f);
        float randomValue = Random.Range(0.5f,1f);
        Vector3 quadSize = new Vector3(randomValue,randomValue) * size;

        float slowDownFactor = Random.Range(2f,2.5f);

        _bloodList.Add(new Particle(pos, dir, _meshParticleSystem, quadSize, Random.Range(0,359f), uvIndex, moveSpeed, slowDownFactor,true));
    }

    public void ClearBllodAndShell(){
        _meshParticleSystem.DestroyAllQuad();
    }
}
