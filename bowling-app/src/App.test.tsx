import { render, screen, waitFor } from '@testing-library/react'
import { afterEach, describe, expect, it, vi } from 'vitest'
import App from './App'

const samplePayload = [
  {
    bowlerFirstName: 'Barbara',
    bowlerMiddleInit: null,
    bowlerLastName: 'Fournier',
    teamName: 'Marlins',
    bowlerAddress: '67 Willow Drive',
    bowlerCity: 'Bothell',
    bowlerState: 'WA',
    bowlerZip: '98123',
    bowlerPhoneNumber: '(206) 555-9876',
  },
]

describe('App', () => {
  afterEach(() => {
    vi.restoreAllMocks()
  })

  it('renders the assignment heading component text', () => {
    vi.stubGlobal(
      'fetch',
      vi.fn(() =>
        Promise.resolve({
          ok: true,
          json: () => Promise.resolve(samplePayload),
        } as Response)
      )
    )

    render(<App />)

    expect(screen.getByText('Bowling League Bowlers')).toBeInTheDocument()
    expect(
      screen.getByText(/Contact and team information for bowlers on the Marlins and Sharks teams/i)
    ).toBeInTheDocument()
  })

  it('renders the required table columns and data from the API', async () => {
    vi.stubGlobal(
      'fetch',
      vi.fn(() =>
        Promise.resolve({
          ok: true,
          json: () => Promise.resolve(samplePayload),
        } as Response)
      )
    )

    render(<App />)

    expect(await screen.findByRole('table', { name: 'Bowlers table' })).toBeInTheDocument()
    expect(screen.getByText('Bowler Name')).toBeInTheDocument()
    expect(screen.getByText('Team Name')).toBeInTheDocument()
    expect(screen.getByText('Address')).toBeInTheDocument()
    expect(screen.getByText('City')).toBeInTheDocument()
    expect(screen.getByText('State')).toBeInTheDocument()
    expect(screen.getByText('Zip')).toBeInTheDocument()
    expect(screen.getByText('Phone Number')).toBeInTheDocument()
    expect(screen.getByText('Barbara Fournier')).toBeInTheDocument()
    expect(screen.getByText('Marlins')).toBeInTheDocument()
  })

  it('shows an error message when the API call fails', async () => {
    vi.stubGlobal(
      'fetch',
      vi.fn(() =>
        Promise.resolve({
          ok: false,
          status: 500,
        } as Response)
      )
    )

    render(<App />)

    await waitFor(() => {
      expect(screen.getByText('Unable to load bowler data from the API.')).toBeInTheDocument()
    })
  })
})
