import React, { useEffect, useState } from 'react'
import { cakeModel } from '../../../Interface';
import CakeCard from './CakeCard';

function CakeList() {
    const [cakes, setCakes] = useState<cakeModel[]>([]);

    useEffect(() => {
      fetch("https://barelycookingapi.azurewebsites.net/api/Cake")
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setCakes(data.result);
      })
    }, []);

    return (
        <div className="container row">
        {cakes.length > 0 && cakes.map((cake, index) => (
          <CakeCard cake = {cake} key={index} />
        ))}
        </div>
    )
}

export default CakeList