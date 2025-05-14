using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTextureChangeController : MonoBehaviour
{
	 public SkinnedMeshRenderer enemyMesh;
	 public List<Material> enemyMaterial;

	 private void Start()
	 {
		  Material enemyMat = enemyMesh.material;
		  enemyMat = enemyMaterial[Random.Range(0, enemyMaterial.Count)];
		  enemyMesh.material = enemyMat;

	 }
}
