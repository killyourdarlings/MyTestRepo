using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPeople : MonoBehaviour
{

    public GameObject[] personPrefabs;
    public int numberOfPeople;
    public Vector3 minAreaPosition;
    public Vector3 maxAreaPosition;

    public float minimumDistanceBetweenPeople;

    public GameObject[] towels;

    GameObject[] people;
    int maxPeople;

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        maxPeople = (int)(((maxAreaPosition.x - minAreaPosition.x) * (maxAreaPosition.z - minAreaPosition.z)));
        if (numberOfPeople >= maxPeople - 3)
        {
            throw new System.Exception("NumberOfPeople cannot fit into designated area. Widen the area, lower the number of people, or decrease minimumDistanceBetweenPeople");
        }

        people = new GameObject[numberOfPeople];
        for (int i = 0; i < numberOfPeople; i++)
        {
            Vector3 pos = new Vector3(Random.Range(minAreaPosition.x, maxAreaPosition.x), 0, Random.Range(minAreaPosition.z, maxAreaPosition.z));

            for (int j = 0; j < i; j++)
            {
                float dist = Vector3.Distance(pos, people[j].transform.position);
                if (dist < minimumDistanceBetweenPeople)
                {
                    j = -1;
                    pos = new Vector3(Random.Range(minAreaPosition.x, maxAreaPosition.x), 0, Random.Range(minAreaPosition.z, maxAreaPosition.z));
                }
            }

            GameObject g = Instantiate(personPrefabs[Random.Range(0, personPrefabs.Length)], pos, Quaternion.Euler(new Vector3(90, 0, Random.Range(-180, 180))));
            people[i] = g;
        }

        //Instantiate towels under people
        for (int i = 0; i < people.Length; i++)
        {
            Instantiate(towels[Random.Range(0, towels.Length)], people[i].transform.position, Quaternion.Euler(new Vector3(90, 0, Random.Range(-180, 180))));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(minAreaPosition, new Vector3(minAreaPosition.x, 0, maxAreaPosition.z));
        Gizmos.DrawLine(new Vector3(minAreaPosition.x, 0, maxAreaPosition.z), maxAreaPosition);
        Gizmos.DrawLine(maxAreaPosition, new Vector3(maxAreaPosition.x, 0, minAreaPosition.z));
        Gizmos.DrawLine(new Vector3(maxAreaPosition.x, 0, minAreaPosition.z), minAreaPosition);
    }
}
