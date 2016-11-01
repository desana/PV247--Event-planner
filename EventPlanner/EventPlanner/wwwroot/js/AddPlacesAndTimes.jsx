class PlacesApp extends React.Component {
    constructor(props) {
        super(props);
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.state = { items: [], text: '' };
    }

    render() {
        return (
      <div>
          <label htmlFor="fieldPlaces">Places</label>
          <input className="form-control" id="fieldPlaces" onChange={this.handleChange} value={this.state.text} />
          <button className="btn btn-default">{'Add another place'}</button>
      </div>
    );
    }

    handleChange(e) {
        this.setState({ text: e.target.value });
    }

    handleSubmit(e) {
        e.preventDefault();
        var newItem = {
            text: this.state.text,
            id: Date.now()
        };
        this.setState((prevState) => ({
            items: prevState.items.concat(newItem),
            text: ''
        }));
    }
}

ReactDOM.render(<PlacesApp />, places);