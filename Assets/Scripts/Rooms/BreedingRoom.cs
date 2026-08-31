using System.Collections.Generic;
using UnityEngine;

public class BreedingRoom : Room
{
    [System.Serializable]
    public struct CreaturePrefabMapping
    {
        public string speciesIdentifier;
        public GameObject prefab;
    }

    [Header("Multi-Species Setup")]
    [SerializeField] private List<CreaturePrefabMapping> speciesPrefabs = new List<CreaturePrefabMapping>();
    [SerializeField] private GameObject fallbackCreaturePrefab;

    [Header("Mutation Configuration")]
    [Range(0f, 100f)][SerializeField] private float mutationChancePercent = 15f;
    [SerializeField] private Color[] mutationColors = new Color[] { Color.magenta, Color.cyan, Color.yellow };

    [Header("Cooldown Settings")]
    [Tooltip("Time in seconds the room must wait before breeding can occur again.")]
    [SerializeField] private float breedingCooldown = 10f;
    private float cooldownTimer = 0f;

    public override void Start()
    {
        base.Start();
        creatureCap = 2;
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        // Tick down the room cooldown timer
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            return; // Exit early while on cooldown
        }

        EvaluateBreedingConditions();
    }

    private void EvaluateBreedingConditions()
    {
        // Criteria 1: EXACTLY 2 creatures in room AND cooldown finished
        if (children.Count == 2 && cooldownTimer <= 0f)
        {
            ExecuteBreedingProcess();
            cooldownTimer = breedingCooldown; // Set cooldown immediately after birth
        }
        else if (children.Count > 2)
        {
            Debug.LogWarning($"[{roomName}] Room is overcrowded ({children.Count} creatures). Breeding halted.");
        }
    }

    private void ExecuteBreedingProcess()
    {
        Transform parentA = children[0];
        Transform parentB = children[1];

        CreatureGenes genesA = parentA.GetComponent<CreatureGenes>();
        CreatureGenes genesB = parentB.GetComponent<CreatureGenes>();

        if (genesA == null) genesA = parentA.gameObject.AddComponent<CreatureGenes>();
        if (genesB == null) genesB = parentB.gameObject.AddComponent<CreatureGenes>();

        // 1. Pick species prefab
        Transform chosenParent = (Random.value > 0.5f) ? parentA : parentB;
        GameObject prefabToSpawn = DeterminePrefabForCreature(chosenParent.gameObject);

        // 2. Instantiate offspring at room movePoint
        Vector3 spawnPosition = (movePoint != null) ? movePoint.position : transform.position;

        // Add a slight position offset so offspring doesn't overlap perfectly with parents
        spawnPosition += new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));

        GameObject child = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // 3. Parent child to room
        child.transform.SetParent(this.transform);

        CreatureGenes childGenes = child.GetComponent<CreatureGenes>();
        if (childGenes == null) childGenes = child.AddComponent<CreatureGenes>();

        // --- TRAIT 1: Color Inheritance & Mutation ---
        if (Random.Range(0f, 100f) <= mutationChancePercent && mutationColors.Length > 0)
        {
            childGenes.bodyColor = mutationColors[Random.Range(0, mutationColors.Length)];
        }
        else
        {
            childGenes.bodyColor = (Random.value > 0.5f) ? genesA.bodyColor : genesB.bodyColor;
        }

        // --- TRAIT 2: Scale Inheritance & Mutation ---
        if (Random.Range(0f, 100f) <= mutationChancePercent)
        {
            float randomScale = Random.Range(0.6f, 1.4f);
            childGenes.bodyScale = new Vector3(randomScale, randomScale, randomScale);
        }
        else
        {
            childGenes.bodyScale = (Random.value > 0.5f) ? genesA.bodyScale : genesB.bodyScale;
        }

        childGenes.ApplyVisuals();
    }

    private GameObject DeterminePrefabForCreature(GameObject parent)
    {
        foreach (var mapping in speciesPrefabs)
        {
            if (!string.IsNullOrEmpty(mapping.speciesIdentifier) && parent.name.Contains(mapping.speciesIdentifier))
            {
                return mapping.prefab;
            }
        }
        return fallbackCreaturePrefab != null ? fallbackCreaturePrefab : speciesPrefabs[0].prefab;
    }
}