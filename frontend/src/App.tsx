import React from 'react';
import { useEffect, useState } from "react";
import logo from './logo.svg';
import './App.css';

function App() {

  const [animals, setAnimals] = useState<string[]>([]);
  
  useEffect(() => {
    fetch("http://localhost:5042/animal")
      .then((res) => res.json())
      .then((data: string[])=> setAnimals(data))
      .catch((err) => console.error(err))
  },[]);

  return (
    <div style={{ padding: 20 }}>
      <h1>Animal Kingdom</h1>
      <ul>
        {animals.map((a, i) => (
          <li key={i}>{a}</li>
        ))}
      </ul>
    </div>
  );
}

export default App;
