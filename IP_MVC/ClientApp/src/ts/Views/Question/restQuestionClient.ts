export async function getOptions(id:string) {
    try {
        let response = await fetch("https://localhost:7292/Question/GetOptions?id=" + id)
        if (!response.ok){
            throw Error(`Unable to get options: ${response.status} ${response.statusText}`)
        }
        return response.json()
    } catch (e) {
        console.error(e)
        throw e
    }
}