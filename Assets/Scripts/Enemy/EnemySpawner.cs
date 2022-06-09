using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Header("Instance Reusing")]

    private bool _isActive = false;

    [Header("Stats")]

    [SerializeField] private float _distanceToActivate = 50;
    [SerializeField] private float _delayBetweenChecks = 0.2f;
    // Make scriptable object to save prefabs?
    [SerializeField] private GameObject[] enemyTypePrefab;
    private EnemyInfo[] enemiesToSpawn;

    private void Start() {
        // sets itself as parent
        for (int i = 0; i < enemiesToSpawn.Length; i++) {
            enemiesToSpawn[i].dataScript = Instantiate(enemyTypePrefab[(int)enemiesToSpawn[i].type], enemiesToSpawn[i].spawnPoint, Quaternion.identity, transform).GetComponent<EnemyData>();
            enemiesToSpawn[i].dataScript.Activate(false);
        }

        InvokeRepeating(nameof(CheckPlayerDistance), 0, _delayBetweenChecks);
    }

    private void CheckPlayerDistance() {
        if (Vector3.Distance(PlayerData.transformPlayer.position, transform.position) <= _distanceToActivate) {
            if (!_isActive) for (int i = 0; i < enemiesToSpawn.Length; i++) enemiesToSpawn[i].dataScript.Activate(true);
        }
        else if (_isActive) for (int i = 0; i < enemiesToSpawn.Length; i++) enemiesToSpawn[i].dataScript.Activate(false);
    }

}

public class EnemyInfo {

    [HideInInspector] public EnemyData dataScript; //
    public EnemyType type;
    public Vector3 spawnPoint;

}

public enum EnemyType {

    Kabu, // Akakabu?
    Ninjin,
    Daikon

}
