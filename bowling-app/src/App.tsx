import { useEffect, useState } from 'react'
import BowlerTable from './components/BowlerTable'
import Heading from './components/Heading'
import type { Bowler } from './types/Bowler'

const API_BASE = import.meta.env.VITE_API_URL ?? 'http://localhost:5010'

function App() {
  const [bowlers, setBowlers] = useState<Bowler[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  useEffect(() => {
    const loadBowlers = async () => {
      try {
        // The React app consumes the ASP.NET API endpoint for assignment data.
        const response = await fetch(`${API_BASE}/api/bowlers`)

        if (!response.ok) {
          throw new Error(`Request failed with status ${response.status}`)
        }

        const data = (await response.json()) as Bowler[]
        setBowlers(data)
      } catch {
        setError('Unable to load bowler data from the API.')
      } finally {
        setLoading(false)
      }
    }

    void loadBowlers()
  }, [])

  return (
    <main className="page">
      <Heading />
      {loading && <p>Loading bowlers...</p>}
      {error && <p className="error">{error}</p>}
      {!loading && !error && <BowlerTable bowlers={bowlers} />}
    </main>
  )
}

export default App
