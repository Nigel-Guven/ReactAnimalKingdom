import React, { useEffect, useState } from "react";
import "./App.css";

// Match backend JSON exactly
interface Animal {
  Id: number;
  Name: string;
  LatinName: string;
  Type: string;
  Location: string[];
  Status: "Safe" | "Vulnerable" | "Endangered" | "Extinct";
}

function App() {
  const [animals, setAnimals] = useState<Animal[]>([]);
  const [search, setSearch] = useState("");

  useEffect(() => {
    fetch("http://localhost:5042/animal")
      .then((res) => res.json())
      .then((data: Animal[]) => {
        setAnimals(data);
      })
      .catch((err) => console.error("Error fetching animals:", err));
  }, []);

  const filteredAnimals = animals.filter(
    (a) =>
      a.Name?.toLowerCase().includes(search.toLowerCase()) ||
      a.Type?.toLowerCase().includes(search.toLowerCase())
  );

  const getStatusColor = (status: Animal["Status"]) => {
    switch (status) {
      case "Extinct":
        return "red";
      case "Endangered":
        return "orange";
      case "Vulnerable":
        return "goldenrod";
      case "Safe":
        return "green";
      default:
        return "black";
    }
  };

  return (
    <div style={{ padding: 20 }}>
      <h1>Animal Kingdom</h1>

      <input
        type="text"
        placeholder="Search by name or type"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        style={{ marginBottom: 20, padding: 5, width: "100%" }}
      />

      <table style={{ width: "100%", borderCollapse: "collapse" }}>
        <thead>
          <tr style={{ borderBottom: "2px solid #333" }}>
            <th>Name</th>
            <th>Latin Name</th>
            <th>Type</th>
            <th>Location</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {filteredAnimals.map((animal) => (
            <tr key={animal.Id} style={{ borderBottom: "1px solid #ccc" }}>
              <td>{animal.Name}</td>
              <td><em>{animal.LatinName}</em></td>
              <td>{animal.Type}</td>
              <td>{animal.Location?.join(", ") || "Unknown"}</td>
              <td
                style={{
                  color: getStatusColor(animal.Status),
                  fontWeight: "bold",
                }}
              >
                {animal.Status}
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {filteredAnimals.length === 0 && <p>No animals match your search.</p>}
    </div>
  );
}

export default App;
