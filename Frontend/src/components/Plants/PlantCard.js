
function PlantCard({term, image}) {
    return (
        <div className='col-lg-3 col-md-5 m-3 border border-success rounded shadow' style={{ backgroundColor: "#C9F0B0" }}>
            <a href={`/plants/${term.id}`} style={{textDecoration: "none", color: "black"}}>
                <img className='img-fluid w-50 p-3' src={require(`${image}`)} alt={term} />
                <h4>{term.name}</h4>
                <p className='p-2'>Lorem ipsum dolor sit amet, consectetur adipiscing elit. In blandit risus a tellus eleifend, in bibendum neque posuere.</p>
                <button className="btn mb-3 btn-success">Одбери</button>
            </a>
        </div>
    );
}

export default PlantCard;