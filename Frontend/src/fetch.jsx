async function  GetPersons() {
    // fetch("https://localhost:7067/api/Person/GetPerson").then((response) => (response => data)).catch(error => (error))

    const response = await fetch("https://localhost:7067/api/Person/GetPerson")
    const result = await response.json();
    console.log(result)
}


function FetchButton() {
    return (
        <>
            <button onClick={GetPersons}>Récupérer les données de l'api</button>
        </>
    )
}

export default FetchButton